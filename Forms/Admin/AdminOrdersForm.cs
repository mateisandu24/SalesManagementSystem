using System;
using System.Windows.Forms;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using SalesManagementSystem.Forms.Admin;
using SalesManagementSystem.Forms.Client;
using SalesManagementSystem.Forms.InOut;


namespace SalesManagementSystem.Forms.Admin
{
    public partial class AdminOrdersForm : Form
    {
        private readonly OrderRepository _orderRepo;

        public AdminOrdersForm()
        {
            InitializeComponent();

            _orderRepo = new OrderRepository(ConfigHelper.ConnectionString);

            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                var orders = _orderRepo.GetAllOrdersForAdmin();

                dgvOrders.DataSource = null;
                dgvOrders.DataSource = orders;

                ApplyFormat(dgvOrders);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea comenzilor: " + ex.Message, 
                                "Eroare", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);
            }
        }

        private void ApplyFormat(DataGridView dgvOrders)
        {
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.AllowUserToAddRows = false;
        }
    }
}