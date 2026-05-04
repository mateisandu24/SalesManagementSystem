using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public partial class RegisterForm : Form
    {
        private readonly UserRepository _userRepo;

        public RegisterForm()
        {
            InitializeComponent();
            _userRepo = new UserRepository(ConfigHelper.ConnectionString);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. Validare câmpuri goale
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Te rugăm să completezi toate câmpurile obligatorii!", "Atenție");
                return;
            }

            // 2. Verificarea parolei (Match check)
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Parolele introduse nu coincid!", "Eroare Validare");
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();
                return;
            }

            // 3. Crearea obiectelor Model
            var newUser = new User
            {
                Username = txtUsername.Text.Trim(),
                PasswordHash = txtPassword.Text // Va fi hash-uită în UserRepository
            };

            var newCustomer = new Customer
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            // 4. Salvarea în bază
            try
            {
                bool success = _userRepo.Register(newUser, newCustomer);

                if (success)
                {
                    MessageBox.Show("Contul a fost creat cu succes!", "Succes");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Eroare: Utilizatorul sau email-ul există deja.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare neprevăzută: " + ex.Message);
            }
        }
    }
}