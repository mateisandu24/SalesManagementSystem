using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using SalesManagementSystem.Forms.Admin;
using SalesManagementSystem.Forms.Client;
using SalesManagementSystem.Forms.InOut;

using System;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Admin
{
    public partial class EditProductForm : Form
    {
        private readonly Product _product;
        private readonly ProductRepository _productRepo;

        public EditProductForm(Product product)
        {
            InitializeComponent();
            _product = product;
            _productRepo = new ProductRepository(ConfigHelper.ConnectionString);

            LoadProductData();
        }

        private void LoadProductData()
        {
            txtName.Text = _product.Name;
            txtDescription.Text = _product.Description;
            txtImageUrl.Text = _product.ImageUrl;
            numPrice.Value = _product.Price;
            numStock.Value = _product.Stock;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _product.Name = txtName.Text.Trim();
            _product.Description = txtDescription.Text.Trim();
            _product.ImageUrl = txtImageUrl.Text.Trim();
            _product.Price = numPrice.Value;
            _product.Stock = (int)numStock.Value;

            try
            {
                _productRepo.Update(_product);
                MessageBox.Show("Produsul a fost actualizat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la actualizarea produsului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
