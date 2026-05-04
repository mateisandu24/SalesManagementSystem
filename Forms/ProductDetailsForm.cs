using SalesManagementSystem.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public partial class ProductDetailsForm : Form
    {
        private Product _product;

        public ProductDetailsForm(Product product, Role userRole)
        {
            InitializeComponent();
            _product = product;
            Utils.ThemeManager.ApplyTheme(this);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;


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

            if (userRole == Role.Admin)
            {

                btnAction.Visible = false;
                this.Text = "Vizualizare Produs (Admin)";
            }
            else
            {
                btnAction.Text = "Adaugă în Coș";
                this.Text = "Detalii Produs";
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {

            MessageBox.Show($"{_product.Name} a fost adăugat în coș!", "Succes");
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}