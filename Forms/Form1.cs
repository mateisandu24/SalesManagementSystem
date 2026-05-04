using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public partial class Form1 : Form
    {
        private readonly User _currentUser; // Esențial pentru RBAC
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
            btnImport.Visible = isAdmin; // Doar adminul importă
            this.Text = isAdmin ? "Sales System - Admin Mode" : "Sales System - Shop Mode";
        }

        // Adaugă acest eveniment pentru Detalii Produs
        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var product = (Product)dgvProducts.Rows[e.RowIndex].DataBoundItem;
                var details = new ProductDetailsForm(product, _currentUser.Role);
                details.ShowDialog();
            }
        }
    }
}