using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Net;


namespace SalesManagementSystem
{
    public partial class Form1 : Form
    {
        private readonly User _currentUser; 
        private readonly ProductRepository _productRepo;
        private List<Product> _allProducts = new List<Product>();
        public Form1(User user)
        {
            InitializeComponent();
            Utils.ThemeManager.ApplyTheme(this);

            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _currentUser = user;
            _productRepo = new ProductRepository(ConfigHelper.ConnectionString);

            RefreshProductList();


            if (_currentUser.Role == Role.User)
            {
                btnImport.Visible = false;
                btnDelete.Visible = false;
                btnViewOrders.Visible = false; 
                btnViewCart.Visible = true;    
            }

            else if (_currentUser.Role == Role.Admin)
            {
                btnViewCart.Visible = false;   
                btnViewOrders.Visible = true;  
            }
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportForm importWindow = new ImportForm();

            importWindow.ShowDialog();

            RefreshProductList();
        }
        private void RefreshProductList()
        {
            try
            {
                _allProducts = _productRepo.GetAll();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea tabelului: {ex.Message}");
            }
        }

        private void ApplyFilters()
        {
            var filtered = _allProducts
                .Where(p => string.IsNullOrEmpty(txtSearch.Text) ||
                            p.Name.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();


            if (cmbSort.SelectedIndex == 0)
                filtered = filtered.OrderBy(p => p.Price).ToList();
            else if (cmbSort.SelectedIndex == 1)
                filtered = filtered.OrderByDescending(p => p.Price).ToList();

            dgvProducts.DataSource = filtered;
            ConfigureColumns();


            LoadImagesAsync();
        }

        private async void LoadImagesAsync()
        {
            string wixPrefix = "https://static.wixstatic.com/media/";


            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            var rows = dgvProducts.Rows.Cast<DataGridViewRow>().ToList();

            foreach (DataGridViewRow row in rows)
            {
                var product = row.DataBoundItem as Product;
                if (product != null && !string.IsNullOrEmpty(product.ImageUrl))
                {
                    string fullImageUrl = wixPrefix + product.ImageUrl.Trim();

                    try
                    {
                        using (var client = new System.Net.WebClient())
                        {
                            client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

                            byte[] imageBytes = await client.DownloadDataTaskAsync(fullImageUrl);

                            using (var ms = new System.IO.MemoryStream(imageBytes))
                            {

                                Image img = new Bitmap(Image.FromStream(ms));

                                if (row.DataGridView != null && dgvProducts.Columns.Contains("ImagePreview"))
                                {
                                    row.Cells["ImagePreview"].Value = img;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        System.Diagnostics.Debug.WriteLine($"Eroare la imaginea pentru {product.Name}: {ex.Message}");

                        if (row.DataGridView != null && dgvProducts.Columns.Contains("ImagePreview"))
                        {
                            row.Cells["ImagePreview"].Value = SystemIcons.Error.ToBitmap();
                        }
                    }
                }
            }
        }

        private void ConfigureColumns()
        {

            dgvProducts.RowTemplate.Height = 100;

            foreach (DataGridViewColumn col in dgvProducts.Columns)
            {
                col.Visible = false;
            }

            if (dgvProducts.Columns["Name"] != null)
            {
                dgvProducts.Columns["Name"].Visible = true;
                dgvProducts.Columns["Name"].HeaderText = "Nume Produs";
            }

            if (dgvProducts.Columns["Price"] != null)
            {
                dgvProducts.Columns["Price"].Visible = true;
                dgvProducts.Columns["Price"].HeaderText = "Preț (RON)";
            }

            if (dgvProducts.Columns["ImagePreview"] == null)
            {
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "ImagePreview";
                imgCol.HeaderText = "Previzualizare";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;


                imgCol.Width = 100;

                dgvProducts.Columns.Insert(0, imgCol);
            }
        }
        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var product = (Product)dgvProducts.Rows[e.RowIndex].DataBoundItem;
                var details = new ProductDetailsForm(product, _currentUser.Role);

                details.ShowDialog();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Application.Exit();

            base.OnFormClosed(e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvProducts.SelectedRows.Count>0)
            {
                var selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;

                var confirmResult = MessageBox.Show
                    ($"Ești sigur că vrei să ștergi {selectedProduct.Name}?",
                    "Confirmare Ștergere",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    _productRepo.Delete(selectedProduct.Id);

                    MessageBox.Show($"{selectedProduct.Name} a fost șters cu succes!", "Șters");

                    RefreshProductList();
                }
            }
            else
            {
                MessageBox.Show("Te rugăm să selectezi un rând întreg din tabel făcând click pe marginea din stânga a rândului.", "Atenție");
            }
        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            Forms.CartForm cartForm = new Forms.CartForm(_currentUser);

            cartForm.ShowDialog();
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            Forms.AdminOrdersForm ordersForm = new Forms.AdminOrdersForm();

            ordersForm.ShowDialog();
        }

    }
}