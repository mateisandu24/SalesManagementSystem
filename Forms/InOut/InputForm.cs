using System;
using System.Windows.Forms;
using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;

namespace SalesManagementSystem.Forms.InOut
{
    public partial class ImportForm : Form
    {
        private readonly ProductRepository _productRepo;

        public ImportForm()
        {
            InitializeComponent();

            _productRepo = new ProductRepository(ConfigHelper.ConnectionString);

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnExecuteImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                MessageBox.Show("Te rugăm să selectezi un fișier mai întâi!", "Atenție");

                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                _productRepo.ImportFromCSV(txtFilePath.Text);

                Cursor = Cursors.Default;

                MessageBox.Show("Importul a fost finalizat cu succes!", "Succes");

                this.Close();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;

                MessageBox.Show($"Eroare la import: {ex.Message}", "Eroare Critică");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}