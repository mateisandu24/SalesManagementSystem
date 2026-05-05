using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
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

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vă rugăm sa reintroduceti usernameul și parola.");

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
                MessageBox.Show("Date invalide!");
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();

            regForm.ShowDialog(); 
        }


    }
}