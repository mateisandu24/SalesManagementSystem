using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using SalesManagementSystem.Forms.Admin;
using SalesManagementSystem.Forms.Client;
using SalesManagementSystem.Forms.InOut;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms.Admin
{
    public partial class AdminCommandsForm : Form
    {
        public AdminCommandsForm()
        {
            InitializeComponent();

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
    }
}
