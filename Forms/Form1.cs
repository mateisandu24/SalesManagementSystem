using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public partial class Form1 : Form
    {
        private readonly User _currentUser; 
        private readonly ProductRepository _productRepo;

        public Form1(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _productRepo = new ProductRepository(Utils.ConfigHelper.ConnectionString);

            SetupPermissions();
        }

        private void SetupPermissions()
        {
            bool isAdmin = _currentUser.Role == Role.Admin;
            btnImport.Visible = isAdmin; 
            this.Text = isAdmin ? "Sales System - Admin Mode" : "Sales System - Shop Mode";
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
                var products = _productRepo.GetAll();

                dgvProducts.DataSource = null;
                dgvProducts.DataSource = products;

                if (dgvProducts.Columns["Id"] != null) dgvProducts.Columns["Id"].Visible = false;
                if (dgvProducts.Columns["Description"] != null) dgvProducts.Columns["Description"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea tabelului: {ex.Message}");
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
    }
}