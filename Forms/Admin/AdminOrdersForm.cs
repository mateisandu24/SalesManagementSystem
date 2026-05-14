using System;
using System.Windows.Forms;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;


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
                var orders = _orderRepo.GetAllOrdersForAdmin();

                dgvOrders.DataSource = null;
                dgvOrders.Columns.Clear();
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