using SalesManagementSystem.Models;
using System;
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


            //lblProductName.Text = product.Name;
            //lblPrice.Text = $"Preț: {product.Price} RON";
            //lblBrand.Text = $"Brand: {product.Brand}";
            //rtbDescription.Text = product.Description;


            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                pbProductImage.SizeMode = PictureBoxSizeMode.Zoom;
                pbProductImage.LoadAsync(product.ImageUrl);
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