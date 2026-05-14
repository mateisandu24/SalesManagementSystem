using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;

namespace SalesManagementSystem.Forms.Client
{
    public partial class CartForm : Form
    {
        private readonly User _currentUser;
        private readonly Services.IImageService _imageService;

        public class CartItemViewModel
        {
            public Product Product { get; set; }
            public string ImageUrl => Product.ImageUrl;
            public string Name => Product.Name;
            public string Description => Product.Description;
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        public CartForm(User user)
        {
            InitializeComponent();

            _currentUser = user;
            _imageService = new Services.ImageService();

            dgvCart.CellDoubleClick += dgvCart_CellDoubleClick;

            LoadCart();
        }

        private void dgvCart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var vm = (CartItemViewModel)dgvCart.Rows[e.RowIndex].DataBoundItem;
                var details = new ProductDetailsForm(vm.Product, _currentUser);

                details.ShowDialog();

                LoadCart();
            }
        }

        private void LoadCart()
        {
            dgvCart.DataSource = null;

            var aggregated = ShoppingCart.Products
                .GroupBy(p => p.Id)
                .Select(g => new CartItemViewModel
                {
                    Product = g.First(),
                    Quantity = g.Count(),
                    Price = g.First().Price * g.Count()
                }).ToList();

            dgvCart.DataSource = aggregated;
            dgvCart.ReadOnly = true;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            ConfigureColumns();
            LoadImagesAsync();

            decimal total = ShoppingCart.Products.Sum(p => p.Price);

            lblTotal.Text = $"Total de plată: {total} RON";
        }

        private async void LoadImagesAsync()
        {
            var rows = dgvCart.Rows.Cast<DataGridViewRow>().ToList();

            foreach (DataGridViewRow row in rows)
            {
                var vm = row.DataBoundItem as CartItemViewModel;
                if (vm != null && !string.IsNullOrEmpty(vm.ImageUrl))
                {
                    try
                    {
                        var img = await _imageService.GetImageAsync(vm.ImageUrl);

                        if (row.DataGridView != null && dgvCart.Columns.Contains("ImagePreview"))
                        {
                            if (img != null)
                            {
                                row.Cells["ImagePreview"].Value = img;
                            }
                            else
                            {
                                row.Cells["ImagePreview"].Value = SystemIcons.Warning.ToBitmap();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (row.DataGridView != null && dgvCart.Columns.Contains("ImagePreview"))
                        {
                            row.Cells["ImagePreview"].Value = SystemIcons.Error.ToBitmap();
                        }
                    }
                }
            }
        }

        private void ConfigureColumns()
        {
            dgvCart.RowTemplate.Height = 100;

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                row.Height = 100;
            }

            foreach (DataGridViewColumn col in dgvCart.Columns)
            {
                col.Visible = false;
            }

            if (dgvCart.Columns["ImagePreview"] == null)
            {
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "ImagePreview";
                imgCol.HeaderText = "Poză Produs";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imgCol.Width = 100;

                Image loadingImg = GetLoadingImage();
                if (loadingImg != null)
                {
                    imgCol.Image = loadingImg;
                    imgCol.DefaultCellStyle.NullValue = loadingImg;
                }
                else
                {
                    imgCol.DefaultCellStyle.NullValue = new Bitmap(1, 1);
                }

                dgvCart.Columns.Insert(0, imgCol);
            }
            else
            {
                dgvCart.Columns["ImagePreview"].Visible = true;
            }

            if (dgvCart.Columns["Name"] != null)
            {
                dgvCart.Columns["Name"].Visible = true;
                dgvCart.Columns["Name"].HeaderText = "Nume Produs";
            }

            if (dgvCart.Columns["Description"] != null)
            {
                dgvCart.Columns["Description"].Visible = true;
                dgvCart.Columns["Description"].HeaderText = "Descriere Produs";
            }
            if (dgvCart.Columns["Quantity"] != null)
            {
                dgvCart.Columns["Quantity"].Visible = true;
                dgvCart.Columns["Quantity"].HeaderText = "Cantitate";
            }

            if (dgvCart.Columns["Price"] != null)
            {
                dgvCart.Columns["Price"].Visible = true;
                dgvCart.Columns["Price"].HeaderText = "Preț Total (RON)";
            }
        }

        private Image GetLoadingImage()
        {
            try
            {
                string localPath = System.IO.Path.Combine(Application.StartupPath, "images", "loading.png");
                if (System.IO.File.Exists(localPath)) return Image.FromFile(localPath);

                string sourcePath = System.IO.Path.Combine(Application.StartupPath, @"..\..\images\loading.png");
                if (System.IO.File.Exists(sourcePath)) return Image.FromFile(sourcePath);
            }
            catch { }
            return null;
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (ShoppingCart.Products.Count == 0)
            {
                MessageBox.Show("Coșul tău este gol!", "Atenție");

                return;
            }

            try
            {
                var orderRepo = new OrderRepository(ConfigHelper.ConnectionString);

                bool success = orderRepo.PlaceOrder(_currentUser.Id, ShoppingCart.Products);

                if (success)
                {
                    MessageBox.Show("Comanda a fost plasată cu succes! Mulțumim!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShoppingCart.Clear();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la procesarea comenzii: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}