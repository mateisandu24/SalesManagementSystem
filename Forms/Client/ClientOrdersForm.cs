using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using SalesManagementSystem.Forms.Admin;
using SalesManagementSystem.Forms.Client;
using SalesManagementSystem.Forms.InOut;

using System;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Client
{
    public partial class ClientOrdersForm : Form
    {
        private readonly User _currentUser;
        private readonly OrderRepository _orderRepo;

        public ClientOrdersForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _orderRepo = new OrderRepository(ConfigHelper.ConnectionString);

            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                dgvOrders.DataSource = _orderRepo.GetOrdersByUserId(_currentUser.Id);
                dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea comenzilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
