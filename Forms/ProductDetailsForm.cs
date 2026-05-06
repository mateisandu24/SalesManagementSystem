using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public partial class ProductDetailsForm : Form
    {
        private Product _product;
        private User _currentUser;
        private NumericUpDown nudQuantity;
        private Button btnBuyNow;
        private Label lblStock;

        public ProductDetailsForm(Product product, User currentUser)
        {
            InitializeComponent();

            _product = product;
            _currentUser = currentUser;

            Utils.ThemeManager.ApplyTheme(this);

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.ClientSize = new Size(475, 440);

            lblProductname.Text = product.Name;
            lblPrice.Text = $"Preț: {product.Price} RON";
            lblBrand.Text = $"Brand: {product.Brand}";
            richTextBox1.Text = product.Description;

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                string wixPrefix = "https://static.wixstatic.com/media/";
                string fullImageUrl = wixPrefix + product.ImageUrl;

                pbProductImage.SizeMode = PictureBoxSizeMode.Zoom;

                try
                {
                    pbProductImage.LoadAsync(fullImageUrl);
                }
                catch
                {
                    pbProductImage.Image = SystemIcons.Error.ToBitmap();
                }
            }

            if ((int)currentUser.Role == (int)Role.Admin)
            {

                btnAction.Visible = false;
                this.Text = "Vizualizare Produs (Admin)";

                // Admin sees stock info but no buy controls
                SetupStockLabel();
            }
            else
            {
                btnAction.Text = "Adaugă în Coș";
                this.Text = "Detalii Produs";

                // Setup buy controls for users
                SetupStockLabel();
                SetupBuyControls();
            }
        }

        private void SetupStockLabel()
        {
            lblStock = new Label
            {
                Name = "lblStock",
                Text = $"Stoc disponibil: {_product.Stock}",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = _product.Stock > 0 ? Color.FromArgb(46, 125, 50) : Color.FromArgb(198, 40, 40),
                Location = new Point(16, 327),
                AutoSize = true
            };
            this.Controls.Add(lblStock);
        }

        private void SetupBuyControls()
        {
            // Quantity label
            var lblQuantity = new Label
            {
                Text = "Cantitate:",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = ThemeManager.TextColor,
                Location = new Point(16, 355),
                AutoSize = true
            };

            // Quantity numeric up/down
            nudQuantity = new NumericUpDown
            {
                Name = "nudQuantity",
                Minimum = 1,
                Maximum = Math.Max(1, _product.Stock),
                Value = 1,
                Location = new Point(100, 353),
                Size = new Size(70, 25),
                Font = new Font("Segoe UI", 10F)
            };

            if (_product.Stock <= 0)
            {
                nudQuantity.Enabled = false;
                nudQuantity.Value = 0;
                nudQuantity.Minimum = 0;
            }

            // Buy Now button
            btnBuyNow = new Button
            {
                Name = "btnBuyNow",
                Text = "🛒 Cumpără Acum",
                Size = new Size(150, 35),
                Location = new Point(16, 390),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = _product.Stock > 0
            };
            btnBuyNow.FlatAppearance.BorderSize = 0;
            btnBuyNow.Click += BtnBuyNow_Click;

            // Reposition existing buttons
            btnAction.Location = new Point(247, 390);
            btnAction.Size = new Size(105, 35);
            btnAction.Enabled = _product.Stock > 0;
            btnBack.Location = new Point(358, 390);
            btnBack.Size = new Size(105, 35);

            this.Controls.Add(lblQuantity);
            this.Controls.Add(nudQuantity);
            this.Controls.Add(btnBuyNow);
        }

        private void BtnBuyNow_Click(object sender, EventArgs e)
        {
            if (_product == null) return;

            int requestedQty = (int)nudQuantity.Value;

            if (requestedQty <= 0)
            {
                MessageBox.Show("Cantitatea trebuie să fie cel puțin 1.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (requestedQty > _product.Stock)
            {
                MessageBox.Show($"Stoc insuficient! Disponibil: {_product.Stock}.", "Stoc Insuficient",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Confirmi cumpărarea a {requestedQty}x \"{_product.Name}\" pentru {_product.Price * requestedQty} RON?",
                "Confirmare Cumpărare",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var orderRepo = new OrderRepository(ConfigHelper.ConnectionString);
                bool success = orderRepo.PlaceOrderDirect(_currentUser.Id, _product, requestedQty);

                if (success)
                {
                    MessageBox.Show("Comanda a fost plasată cu succes! Mulțumim!", "Succes",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Update local product stock
                    _product.Stock -= requestedQty;
                    lblStock.Text = $"Stoc disponibil: {_product.Stock}";
                    lblStock.ForeColor = _product.Stock > 0 ? Color.FromArgb(46, 125, 50) : Color.FromArgb(198, 40, 40);
                    nudQuantity.Maximum = Math.Max(1, _product.Stock);
                    if (_product.Stock <= 0)
                    {
                        nudQuantity.Enabled = false;
                        btnBuyNow.Enabled = false;
                        btnAction.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Stoc insuficient! Altcineva a cumpărat produsul.", "Eroare",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la procesarea comenzii: " + ex.Message, "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if(_product != null)
            {
                ShoppingCart.Add(_product);

                MessageBox.Show($"'{_product.Name}' a fost adăugat în coș!",
                        "Succes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                this.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

    }
}