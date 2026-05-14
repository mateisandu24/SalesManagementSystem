using System;
using System.Windows.Forms;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;

namespace SalesManagementSystem.Forms.Admin
{
    public partial class AdminOrderDetailsForm : Form
    {
        private int _orderId;

        public AdminOrderDetailsForm(int orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            LoadOrderDetails();
        }

        private void LoadOrderDetails()
        {
            try
            {
                var orderRepo = new OrderRepository(ConfigHelper.ConnectionString);
                var items = orderRepo.GetOrderDetails(_orderId);
                dgvOrderItems.DataSource = null;
                dgvOrderItems.Columns.Clear();
                dgvOrderItems.DataSource = items;
                dgvOrderItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvOrderItems.ReadOnly = true;
                dgvOrderItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvOrderItems.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea detaliilor comenzii: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
