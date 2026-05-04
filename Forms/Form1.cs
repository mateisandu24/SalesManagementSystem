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
        private void btnImport_Click(object sender, EventArgs e)
        {
            // Creăm instanța noului formular
            ImportForm importWindow = new ImportForm();

            // Îl deschidem ca Dialog (Userul trebuie să termine importul înainte de a reveni la tabel)
            importWindow.ShowDialog();

            // Opțional: Reîmprospătăm tabelul după ce se închide fereastra de import
            RefreshProductList();
        }
        private void RefreshProductList()
        {
            try
            {
                // 1. Luăm lista proaspătă de produse din baza de date
                var products = _productRepo.GetAll();

                // 2. Resetăm sursa de date a tabelului pentru a forța refresh-ul vizual
                dgvProducts.DataSource = null;
                dgvProducts.DataSource = products;

                // Opțional: Ascundem coloana de ID sau ImageUrl dacă nu vrem să le vedem în tabel
                if (dgvProducts.Columns["Id"] != null) dgvProducts.Columns["Id"].Visible = false;
                if (dgvProducts.Columns["Description"] != null) dgvProducts.Columns["Description"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea tabelului: {ex.Message}");
            }
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