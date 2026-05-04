using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public partial class Form1 : Form
    {

        private readonly string _connectionString = ConfigHelper.ConnectionString;
        private ProductRepository _productRepo;

        public Form1()
        {
            InitializeComponent();
            _productRepo = new ProductRepository(_connectionString);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }


        private void RefreshGrid()
        {
            try
            {
                dgvProducts.DataSource = _productRepo.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la conectarea cu baza de date: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnImport_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Selectează fișierul CSV cu produse";
                ofd.Filter = "CSV Files (*.csv)|*.csv";


                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        btnImport.Enabled = false;

                        _productRepo.ImportFromCSV(ofd.FileName);

                        MessageBox.Show("Produsele au fost importate cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        RefreshGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eroare la importul fișierului: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {

                        btnImport.Enabled = true;
                    }
                }
            }
        }

        public Form1(User user)
        {
            InitializeComponent();
            //_currentUser = user;
            _productRepo = new ProductRepository(ConfigHelper.ConnectionString);

            //SetupPermissions();
        }

        //private void SetupPermissions()
        //{
        //   // bool isAdmin = _currentUser.Role == Role.Admin;

        //    // Admin vede tot
        //   // btnImport.Visible = isAdmin;
        //   // btnDelete.Visible = isAdmin;
        //    btnEdit.Visible = isAdmin;

        //    // Userul vede coșul (Cart)
        //    btnAddToCart.Visible = !isAdmin;
        //    btnViewCart.Visible = !isAdmin;

        //    if (isAdmin)
        //    {
        //        this.Text = "Back Office - Admin Mode";
        //    }
        //    else
        //    {
        //        this.Text = "Shop - User Mode";
        //    }
        //}

    }
}