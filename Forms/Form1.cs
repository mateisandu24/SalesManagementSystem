using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.ComponentModel;
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

            RefreshProductList();

            if (_currentUser.Role == Role.User)
            {
                btnImport.Visible = false;
                btnDelete.Visible = false;
            }

            Utils.ThemeManager.ApplyTheme(this);
            SetupLayout();
        }

        private void SetupLayout()
        {
            this.MinimumSize = new System.Drawing.Size(800, 500);
            dgvProducts.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnImport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            if (btnDelete != null)
            {
                btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            }
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvProducts.SelectedRows.Count>0)
            {
                var selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;

                var confirmResult = MessageBox.Show
                    ($"Ești sigur că vrei să ștergi {selectedProduct.Name}?",
                    "Confirmare Ștergere",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    _productRepo.Delete(selectedProduct.Id);

                    MessageBox.Show($"{selectedProduct.Name} a fost șters cu succes!", "Șters");

                    RefreshProductList();
                }
            }
            else
            {
                MessageBox.Show("Te rugăm să selectezi un rând întreg din tabel făcând click pe marginea din stânga a rândului.", "Atenție");
            }
        }

    }
}