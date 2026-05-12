using System;
using System.Linq;
using System.Windows.Forms;
using SalesManagementSystem.Models;
using SalesManagementSystem.Utils;
using SalesManagementSystem.Forms.Admin;
using SalesManagementSystem.Forms.Client;
using SalesManagementSystem.Forms.InOut;

using SalesManagementSystem.Repositories;

namespace SalesManagementSystem.Forms.Client
{
    public partial class CartForm : Form
    {
        private readonly User _currentUser;

        public CartForm(User user)
        {
            InitializeComponent();

            _currentUser = user;

            LoadCart();
        }

        private void LoadCart()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = ShoppingCart.Products;

            decimal total = ShoppingCart.Products.Sum(p => p.Price);

            lblTotal.Text = $"Total de plată: {total} RON";
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (ShoppingCart.Products.Count == 0)
            {
                MessageBox.Show("Coșul tău este gol!", "Atenție");

                return;
            }

            try
            {
                var orderRepo = new OrderRepository(ConfigHelper.ConnectionString);

                bool success = orderRepo.PlaceOrder(_currentUser.Id, ShoppingCart.Products);

                if (success)
                {
                    MessageBox.Show("Comanda a fost plasată cu succes! Mulțumim!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShoppingCart.Clear();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la procesarea comenzii: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}