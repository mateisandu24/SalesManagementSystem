using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;

namespace SalesManagementSystem.Forms.InOut
{
    public partial class RegisterForm : Form
    {
        private readonly UserRepository _userRepo;

        private static readonly Regex EmailRegex = new Regex(
            @"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled);

        private static readonly Regex PasswordRegex = new Regex(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#+\-_.\(\)\[\]{}])[A-Za-z\d@$!%*?&#+\-_.\(\)\[\]{}]{8,}$",
            RegexOptions.Compiled);

        public RegisterForm()
        {
            InitializeComponent();

            _userRepo = new UserRepository(ConfigHelper.ConnectionString);

        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
            txtConfirmPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Prenumele este obligatoriu!", "Câmp obligatoriu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Numele de familie este obligatoriu!", "Câmp obligatoriu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Adresa de email este obligatorie!", "Câmp obligatoriu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (!EmailRegex.IsMatch(txtEmail.Text.Trim()))
            {
                MessageBox.Show(
                    "Adresa de email nu este validă.\n\nExemplu corect: utilizator@domeniu.com",
                    "Email invalid",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username-ul este obligatoriu!", "Câmp obligatoriu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (txtUsername.Text.Trim().Length < 3)
            {
                MessageBox.Show("Username-ul trebuie să aibă cel puțin 3 caractere.", "Username prea scurt",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Parola este obligatorie!", "Câmp obligatoriu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (!PasswordRegex.IsMatch(txtPassword.Text))
            {
                MessageBox.Show(
                    "Parola nu îndeplinește cerințele de securitate:\n\n" +
                    "• Minim 8 caractere\n" +
                    "• Cel puțin o literă mare (A-Z)\n" +
                    "• Cel puțin o literă mică (a-z)\n" +
                    "• Cel puțin o cifră (0-9)\n" +
                    "• Cel puțin un caracter special (@$!%*?&# etc.)",
                    "Parolă slabă",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Parolele introduse nu coincid!", "Eroare Validare",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtPassword.Clear();
                txtConfirmPassword.Clear();
                txtPassword.Focus();

                return;
            }

            var newUser = new User
            {
                Username = txtUsername.Text.Trim(),
                PasswordHash = txtPassword.Text
            };

            var newCustomer = new Customer
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            try
            {
                bool success = _userRepo.Register(newUser, newCustomer);

                if (success)
                {
                    MessageBox.Show("Contul a fost creat cu succes!", "Succes",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Eroare: Utilizatorul sau email-ul există deja.",
                        "Cont existent", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare neprevăzută: " + ex.Message,
                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}