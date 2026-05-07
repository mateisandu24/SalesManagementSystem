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

            // Setup sort ComboBox items
            cmbSort.Items.Clear();
            cmbSort.Items.Add("Preț: Crescător");
            cmbSort.Items.Add("Preț: Descrescător");
            cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += (s, ev) => ApplyFilters();

            // Wire up search textbox for live filtering
            txtSearch.TextChanged += (s, ev) => ApplyFilters();

            RefreshProductList();

            // Role-based visibility
            if ((int)_currentUser.Role == (int)Role.User)
            {
                btnDelete.Visible = false;
                btnAdminDashboard.Visible = false;
                btnViewCart.Visible = true;
            }
            else if ((int)_currentUser.Role == (int)Role.Admin)
            {
                btnViewCart.Visible = false;
                btnAdminDashboard.Visible = true;
            }
        }

        private void btnAdminDashboard_Click(object sender, EventArgs e)
        {
            var adminForm = new Forms.AdminCommandsForm();
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
                filtered = filtered.OrderBy(p => p.Price).ToList();
            else if (cmbSort.SelectedIndex == 1)
                filtered = filtered.OrderByDescending(p => p.Price).ToList();

            dgvProducts.DataSource = filtered;
            ConfigureColumns();


            LoadImagesAsync();
        }

        private static readonly System.Net.Http.HttpClient _httpClient;

        static Form1()
        {
            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11;

            var handler = new System.Net.Http.HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _httpClient = new System.Net.Http.HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept",
                "image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,ro;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Referer", "https://www.setandglow.ro/");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "image");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "no-cors");
            _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "cross-site");
        }

        /// <summary>
        /// Resolves the ImageUrl value (from CSV/DB) into a full downloadable URL.
        /// Handles: full http(s) URLs, wix:image:// internal format, and raw media hashes.
        /// </summary>
        private string ResolveImageUrl(string imageUrl)
        {
            string trimmed = imageUrl.Trim();

            // Already a full URL
            if (trimmed.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return trimmed;
            }

            // Wix internal format: wix:image://v1/{hash}/{filename}#originWidth=W&originHeight=H
            if (trimmed.StartsWith("wix:image://", StringComparison.OrdinalIgnoreCase))
            {
                // Strip the "wix:image://v1/" prefix
                string path = trimmed.Substring("wix:image://".Length);
                if (path.StartsWith("v1/", StringComparison.OrdinalIgnoreCase))
                    path = path.Substring(3);

                // Remove fragment (#originWidth=...) 
                int hashIdx = path.IndexOf('#');
                if (hashIdx >= 0)
                    path = path.Substring(0, hashIdx);

                // path is now "{hash}/{filename}" — use just the hash part for direct media URL
                string[] parts = path.Split('/');
                string hash = parts[0];

                return "https://static.wixstatic.com/media/" + hash;
            }

            // Raw hash/filename — just prepend prefix; also strip any fragment
            int fragIdx = trimmed.IndexOf('#');
            if (fragIdx >= 0)
                trimmed = trimmed.Substring(0, fragIdx);

            return "https://static.wixstatic.com/media/" + trimmed;
        }

        private async void LoadImagesAsync()
        {
            // Log the first 3 URLs to a debug file so we can inspect the actual values
            string debugLogPath = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "image_debug.log");

            var rows = dgvProducts.Rows.Cast<DataGridViewRow>().ToList();
            int logCount = 0;

            using (var logWriter = new System.IO.StreamWriter(debugLogPath, false))
            {
                logWriter.WriteLine($"[{DateTime.Now}] LoadImagesAsync started — {rows.Count} rows");

                foreach (DataGridViewRow row in rows)
                {
                    var product = row.DataBoundItem as Product;
                    if (product != null && !string.IsNullOrEmpty(product.ImageUrl))
                    {
                        string fullImageUrl = ResolveImageUrl(product.ImageUrl);

                        // Log first few URLs for debugging
                        if (logCount < 5)
                        {
                            logWriter.WriteLine($"  RAW: [{product.ImageUrl}]");
                            logWriter.WriteLine($"  URL: [{fullImageUrl}]");
                            logWriter.WriteLine();
                            logCount++;
                        }

                        try
                        {
                            var response = await _httpClient.GetAsync(fullImageUrl);

                            if (!response.IsSuccessStatusCode)
                            {
                                System.Diagnostics.Debug.WriteLine(
                                    $"HTTP {(int)response.StatusCode} pentru {product.Name}: {fullImageUrl}");

                                if (row.DataGridView != null && dgvProducts.Columns.Contains("ImagePreview"))
                                {
                                    row.Cells["ImagePreview"].Value = SystemIcons.Warning.ToBitmap();
                                }
                                continue;
                            }

                            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                            using (var ms = new System.IO.MemoryStream(imageBytes))
                            {
                                Image img = new Bitmap(Image.FromStream(ms));

                                if (row.DataGridView != null && dgvProducts.Columns.Contains("ImagePreview"))
                                {
                                    row.Cells["ImagePreview"].Value = img;
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

                logWriter.WriteLine($"[{DateTime.Now}] LoadImagesAsync finished");
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

                dgvProducts.Columns.Insert(0, imgCol);
            }
        }
        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var product = (Product)dgvProducts.Rows[e.RowIndex].DataBoundItem;
                var details = new ProductDetailsForm(product, _currentUser);

                details.ShowDialog();

                // Refresh after returning from details (stock may have changed)
                RefreshProductList();
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

    }
}