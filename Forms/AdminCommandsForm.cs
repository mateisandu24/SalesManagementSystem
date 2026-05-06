using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms
{
    public partial class AdminCommandsForm : Form
    {
        public AdminCommandsForm()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);

            this.Text = "Panou Administrare";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(400, 280);

            SetupUI();
        }

        private void SetupUI()
        {
            // Title label
            var lblTitle = new Label
            {
                Text = "Panou Administrare",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = ThemeManager.TextColor,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60
            };

            // Import CSV button
            var btnImportCsv = new Button
            {
                Name = "btnImportCsv",
                Text = "📂  Importă CSV",
                Size = new Size(300, 45),
                Location = new Point(50, 80),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeManager.PrimaryColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnImportCsv.FlatAppearance.BorderSize = 0;
            btnImportCsv.Click += BtnImportCsv_Click;

            // View Orders button
            var btnViewOrders = new Button
            {
                Name = "btnViewOrders",
                Text = "📋  Vezi Comenzi Clienți",
                Size = new Size(300, 45),
                Location = new Point(50, 140),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(41, 50, 65),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnViewOrders.FlatAppearance.BorderSize = 0;
            btnViewOrders.Click += BtnViewOrders_Click;

            // Back button
            var btnBack = new Button
            {
                Name = "btnBack",
                Text = "Înapoi",
                Size = new Size(300, 40),
                Location = new Point(50, 200),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(200, 200, 200),
                ForeColor = ThemeManager.TextColor,
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += (s, e) => this.Close();

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnImportCsv);
            this.Controls.Add(btnViewOrders);
            this.Controls.Add(btnBack);
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
