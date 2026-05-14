using System;
using System.Windows.Forms;
using SalesManagementSystem.Forms.Admin;
using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;

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

            dgvOrders.CellDoubleClick += DgvOrders_CellDoubleClick;
        }

        private void DgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvOrders.Rows[e.RowIndex];
                if (row.Cells["Număr Comandă"].Value != null)
                {
                    int orderId = Convert.ToInt32(row.Cells["Număr Comandă"].Value);
                    var detailsForm = new AdminOrderDetailsForm(orderId);
                    detailsForm.ShowDialog();
                }
            }
        }

        private void LoadOrders()
        {
            try
            {
                dgvOrders.DataSource = null;
                dgvOrders.Columns.Clear();
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
