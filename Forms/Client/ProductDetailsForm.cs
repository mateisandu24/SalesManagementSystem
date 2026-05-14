using System;
using System.Drawing;
using System.Windows.Forms;
using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;

namespace SalesManagementSystem.Forms.Client
{
    public partial class ProductDetailsForm : Form
    {
        private Product _product;
        private User _currentUser;
        public ProductDetailsForm(Product product, User currentUser)
        {
            InitializeComponent();

            _product = product;
            _currentUser = currentUser;

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
            SetupStockLabel();

            if ((int)currentUser.Role == (int)Role.Admin)
            {
                btnAction.Visible = false;
                btnBuyNow.Visible = false;
                nudQuantity.Visible = false;
                lblQuantity.Visible = false;
                this.Text = "Vizualizare Produs (Admin) — SalesManagementSystem";
            }
            else
            {
                this.Text = "Detalii Produs — SalesManagementSystem";
                SetupBuyControls();

                int cartQty = ShoppingCart.GetQuantity(product.Id);
                if (cartQty > 0)
                {
                    nudQuantity.Value = cartQty;
                    btnAction.Text = "Actualizează Coșul";
                }
                else
                {
                    btnAction.Text = "Adaugă în Coș";
                }
            }
        }

        private void SetupStockLabel()
        {
            lblStock.Text = $"Stoc disponibil: {_product.Stock}";
            lblStock.ForeColor = _product.Stock > 0 ? Color.FromArgb(46, 125, 50) : Color.FromArgb(198, 40, 40);
        }

        private void SetupBuyControls()
        {
            lblQuantity.Location = new Point(richTextBox1.Left, btnAction.Top - 35);

            nudQuantity.Minimum = 0;
            nudQuantity.Maximum = Math.Max(1, _product.Stock);
            nudQuantity.Value = 1;
            nudQuantity.Location = new Point(richTextBox1.Left + 80, btnAction.Top - 38);

            if (_product.Stock <= 0)
            {
                nudQuantity.Enabled = false;
                nudQuantity.Minimum = 0;
                nudQuantity.Value = 0;

            }

            btnBuyNow.Enabled = _product.Stock > 0;
            btnBuyNow.Location = new Point(richTextBox1.Left, btnAction.Top);

            btnAction.Enabled = _product.Stock > 0;
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
            if (_product != null)
            {
                int qty = (int)nudQuantity.Value;
                if (qty == 0)
                {
                    ShoppingCart.Remove(_product.Id);
                    MessageBox.Show($"'{_product.Name}' a fost sters din coș!",
                            "Succes",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                }
                else
                {
                    ShoppingCart.SetQuantity(_product, qty);
                    MessageBox.Show($"Cantitatea pentru '{_product.Name}' a fost actualizată în coș!",
                            "Succes",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                }

                this.Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}