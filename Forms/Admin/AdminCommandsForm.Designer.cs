namespace SalesManagementSystem.Forms.Admin
{
    partial class AdminCommandsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnImportCsv = new System.Windows.Forms.Button();
            this.btnViewOrders = new System.Windows.Forms.Button();
            this.btnDeleteAllProducts = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblSales = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Panou Administrare";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnImportCsv
            // 
            this.btnImportCsv.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnImportCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportCsv.FlatAppearance.BorderSize = 0;
            this.btnImportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportCsv.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnImportCsv.ForeColor = System.Drawing.Color.White;
            this.btnImportCsv.Location = new System.Drawing.Point(50, 80);
            this.btnImportCsv.Name = "btnImportCsv";
            this.btnImportCsv.Size = new System.Drawing.Size(300, 45);
            this.btnImportCsv.TabIndex = 1;
            this.btnImportCsv.Text = "📂  Importă CSV";
            this.btnImportCsv.UseVisualStyleBackColor = false;
            this.btnImportCsv.Click += new System.EventHandler(this.BtnImportCsv_Click);
            // 
            // btnViewOrders
            // 
            this.btnViewOrders.BackColor = System.Drawing.Color.FromArgb(41, 50, 65);
            this.btnViewOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewOrders.FlatAppearance.BorderSize = 0;
            this.btnViewOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewOrders.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnViewOrders.ForeColor = System.Drawing.Color.White;
            this.btnViewOrders.Location = new System.Drawing.Point(50, 140);
            this.btnViewOrders.Name = "btnViewOrders";
            this.btnViewOrders.Size = new System.Drawing.Size(300, 45);
            this.btnViewOrders.TabIndex = 2;
            this.btnViewOrders.Text = "📋  Vezi Comenzi Clienți";
            this.btnViewOrders.UseVisualStyleBackColor = false;
            this.btnViewOrders.Click += new System.EventHandler(this.BtnViewOrders_Click);
            // 
            // btnDeleteAllProducts
            // 
            this.btnDeleteAllProducts.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnDeleteAllProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteAllProducts.FlatAppearance.BorderSize = 0;
            this.btnDeleteAllProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAllProducts.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnDeleteAllProducts.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAllProducts.Location = new System.Drawing.Point(50, 200);
            this.btnDeleteAllProducts.Name = "btnDeleteAllProducts";
            this.btnDeleteAllProducts.Size = new System.Drawing.Size(300, 45);
            this.btnDeleteAllProducts.TabIndex = 4;
            this.btnDeleteAllProducts.Text = "🗑️  Șterge Toate Produsele";
            this.btnDeleteAllProducts.UseVisualStyleBackColor = false;
            this.btnDeleteAllProducts.Click += new System.EventHandler(this.BtnDeleteAllProducts_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnBack.Location = new System.Drawing.Point(50, 260);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(300, 45);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Înapoi";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            //
            // lblSales
            //
            this.lblSales.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblSales.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.lblSales.Location = new System.Drawing.Point(50, 320);
            this.lblSales.Name = "lblSales";
            this.lblSales.Size = new System.Drawing.Size(300, 45);
            this.lblSales.TabIndex = 5;
            this.lblSales.Text = "Vândut în ultimele 30 zile: ...";
            this.lblSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AdminCommandsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 380);
            this.Controls.Add(this.lblSales);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDeleteAllProducts);
            this.Controls.Add(this.btnViewOrders);
            this.Controls.Add(this.btnImportCsv);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AdminCommandsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Panou Administrare — SalesManagementSystem";
            
            // Theme Settings
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.ForeColor = System.Drawing.Color.FromArgb(43, 45, 66);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.btnImportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportCsv.FlatAppearance.BorderSize = 0;
            this.btnImportCsv.BackColor = System.Drawing.Color.FromArgb(67, 97, 238);
            this.btnImportCsv.ForeColor = System.Drawing.Color.White;
            this.btnImportCsv.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnImportCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewOrders.FlatAppearance.BorderSize = 0;
            this.btnViewOrders.BackColor = System.Drawing.Color.FromArgb(67, 97, 238);
            this.btnViewOrders.ForeColor = System.Drawing.Color.White;
            this.btnViewOrders.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnViewOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(67, 97, 238);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(43, 45, 66);

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnImportCsv;
        private System.Windows.Forms.Button btnViewOrders;
        private System.Windows.Forms.Button btnDeleteAllProducts;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblSales;
    }
}
