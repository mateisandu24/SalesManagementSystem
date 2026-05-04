using SalesManagementSystem.Models;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepo;

        public LoginForm()
        {
            InitializeComponent();
            _userRepo = new UserRepository(ConfigHelper.ConnectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please enter both username and password.");
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
                MessageBox.Show("Invalid credentials!");
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Deschidem formularul de înregistrare
            RegisterForm regForm = new RegisterForm();
            regForm.ShowDialog(); // ShowDialog forțează utilizatorul să termine înregistrarea sau să închidă fereastra
        }


    }
}