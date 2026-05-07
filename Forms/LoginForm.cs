using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepo;

        public LoginForm()
        {
            InitializeComponent();

            _userRepo = new UserRepository(ConfigHelper.ConnectionString);

            Utils.ThemeManager.ApplyTheme(this);

            SetupShowPasswordToggle();
        }

        private void SetupShowPasswordToggle()
        {
            var chkShowPassword = new CheckBox
            {
                Text = "Arată parola",
                Location = new Point(30, 170),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = ThemeManager.TextColor
            };
            chkShowPassword.CheckedChanged += (s, ev) =>
            {
                txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
            };
            this.Controls.Add(chkShowPassword);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show("Vă rugăm să introduceți username-ul.", "Câmp obligatoriu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vă rugăm să introduceți parola.", "Câmp obligatoriu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            User loggedUser = _userRepo.Authenticate(user, pass);

            if (loggedUser != null)
            {
                Form1 mainForm = new Form1(loggedUser);

                mainForm.Show();
                
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("Username sau parolă incorectă. Vă rugăm să încercați din nou.",
                    "Autentificare eșuată", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();

            regForm.ShowDialog(); 
        }
    }
}