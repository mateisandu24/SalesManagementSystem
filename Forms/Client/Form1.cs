using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesManagementSystem.Forms.Admin;
using SalesManagementSystem.Forms.InOut;
using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;


namespace SalesManagementSystem.Forms.Client
{
    public partial class Form1 : Form
    {
        private readonly User _currentUser;
        private readonly ProductRepository _productRepo;
        private readonly Services.IImageService _imageService;
        private List<Product> _allProducts = new List<Product>();

        public Form1(User user)
        {
            InitializeComponent();

            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _currentUser = user;
            _productRepo = new ProductRepository(ConfigHelper.ConnectionString);
            _imageService = new Services.ImageService();

            cmbSort.Items.Clear();
            cmbSort.Items.Add("Preț: Crescător");
            cmbSort.Items.Add("Preț: Descrescător");
            cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += (s, ev) => ApplyFilters();

            txtSearch.TextChanged += (s, ev) => ApplyFilters();

            RefreshProductList();

            if ((int)_currentUser.Role == (int)Role.User)
            {
                ApplyUserUI();
            }

            else if ((int)_currentUser.Role == (int)Role.Admin)
            {
                ApplyAdminUI();
            }
        }

        private void ApplyUserUI()
        {
            btnDelete.Visible = false;
            btnEditProduct.Visible = false;
            btnAdminDashboard.Visible = false;
            btnViewCart.Visible = true;
            btnClientOrders.Visible = true;
        }

        private void ApplyAdminUI()
        {
            btnViewCart.Visible = false;
            btnClientOrders.Visible = false;
            btnAdminDashboard.Visible = true;
            btnEditProduct.Visible = true;
            btnDelete.Visible = true;
        }

        private void btnAdminDashboard_Click(object sender, EventArgs e)
        {
            var adminForm = new AdminCommandsForm();
            adminForm.ShowDialog();
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
            {
                filtered = filtered.OrderBy(p => p.Price).ToList();
            }
            else if (cmbSort.SelectedIndex == 1)
            {
                filtered = filtered.OrderByDescending(p => p.Price).ToList();
            }


            dgvProducts.DataSource = filtered;

            ConfigureColumns();

            LoadImagesAsync();
        }

        private async void LoadImagesAsync()
        {
            var rows = dgvProducts.Rows.Cast<DataGridViewRow>().ToList();

            foreach (DataGridViewRow row in rows)
            {
                var product = row.DataBoundItem as Product;
                if (product != null && !string.IsNullOrEmpty(product.ImageUrl))
                {
                    try
                    {
                        var img = await _imageService.GetImageAsync(product.ImageUrl);

                        if (row.DataGridView != null && dgvProducts.Columns.Contains("ImagePreview"))
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

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                row.Height = 100;
            }

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

            if (dgvProducts.Columns["Stock"] != null)
            {
                dgvProducts.Columns["Stock"].Visible = true;
                dgvProducts.Columns["Stock"].HeaderText = "Stoc";
            }

            if (dgvProducts.Columns["ImagePreview"] == null)
            {
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "ImagePreview";
                imgCol.HeaderText = "Previzualizare";
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

                dgvProducts.Columns.Insert(0, imgCol);
            }
            else
            {
                dgvProducts.Columns["ImagePreview"].Visible = true;
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
        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var product = (Product)dgvProducts.Rows[e.RowIndex].DataBoundItem;
                var details = new ProductDetailsForm(product, _currentUser);

                details.ShowDialog();

                RefreshProductList();
            }
        }

        private bool _isLoggingOut = false;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (!_isLoggingOut)
            {
                Application.Exit();
            }

            base.OnFormClosed(e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
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
            CartForm cartForm = new CartForm(_currentUser);

            cartForm.ShowDialog();
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                var selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;
                var editForm = new EditProductForm(selectedProduct);

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshProductList();
                }
            }
            else
            {
                MessageBox.Show("Te rugăm să selectezi un rând întreg din tabel făcând click pe marginea din stânga a rândului.", "Atenție");
            }
        }

        private void btnClientOrders_Click(object sender, EventArgs e)
        {
            ClientOrdersForm clientOrdersForm = new ClientOrdersForm(_currentUser);
            clientOrdersForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _isLoggingOut = true;
            var loginForm = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();

            if (loginForm != null)
            {
                loginForm.Show();
                var txtPasswordControl = loginForm.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;
                if (txtPasswordControl != null) txtPasswordControl.Clear();
            }
            else
            {
                new LoginForm().Show();
            }

            this.Close();
        }

    }
}