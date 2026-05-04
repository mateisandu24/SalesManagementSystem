using SalesManagementSystem.Repositories;
using SalesManagementSystem.Utils;
using System;
using System.Windows.Forms;

namespace SalesManagementSystem
{
    public partial class ImportForm : Form
    {
        private readonly ProductRepository _productRepo;

        public ImportForm()
        {
            InitializeComponent();
            // Inițializăm repository-ul folosind connection string-ul din ConfigHelper
            _productRepo = new ProductRepository(ConfigHelper.ConnectionString);
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
                Cursor = Cursors.WaitCursor; // Arătăm că aplicația lucrează

                // Apelăm metoda de import pe care ai definit-o deja în ProductRepository
                _productRepo.ImportFromCSV(txtFilePath.Text);

                Cursor = Cursors.Default;
                MessageBox.Show("Importul a fost finalizat cu succes!", "Succes");
                this.Close(); // Închidem formularul după succes
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