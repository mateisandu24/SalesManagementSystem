using System;
using System.Windows.Forms;
using SalesManagementSystem.Forms.InOut;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;

namespace SalesManagementSystem.Forms.Admin
{
    public partial class AdminCommandsForm : Form
    {
        public AdminCommandsForm()
        {
            InitializeComponent();
            LoadSalesData();
        }

        private void LoadSalesData()
        {
            try
            {
                var orderRepo = new OrderRepository(ConfigHelper.ConnectionString);
                decimal sales = orderRepo.GetSalesLastNDays(30);
                lblSales.Text = $"Vândut în ultimele 30 zile: {sales:0.00} RON";
            }
            catch (Exception ex)
            {
                lblSales.Text = "Vândut în ultimele 30 zile: Eroare";
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnImportCsv_Click(object sender, EventArgs e)
        {
            var importWindow = new ImportForm();
            importWindow.ShowDialog();
        }

        private void BtnViewOrders_Click(object sender, EventArgs e)
        {
            var ordersForm = new AdminOrdersForm();
            ordersForm.ShowDialog();
        }

        private void BtnDeleteAllProducts_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Ești sigur că vrei să ștergi TOATE produsele? Această acțiune este ireversibilă!",
                "Confirmare Ștergere Toate Produsele",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var productRepo = new ProductRepository(ConfigHelper.ConnectionString);
                    productRepo.DeleteAll();
                    MessageBox.Show("Toate produsele au fost șterse cu succes!", "Ștergere Completă", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Eroare la ștergerea produselor: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
