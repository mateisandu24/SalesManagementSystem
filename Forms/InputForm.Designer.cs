namespace SalesManagementSystem
{
    partial class ImportForm
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
            this.lblImportCSV = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnExecuteImport = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblImportCSV
            // 
            this.lblImportCSV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblImportCSV.Location = new System.Drawing.Point(0, 15);
            this.lblImportCSV.Name = "lblImportCSV";
            this.lblImportCSV.Size = new System.Drawing.Size(430, 30);
            this.lblImportCSV.TabIndex = 0;
            this.lblImportCSV.Text = "Importă Fișierul CSV";
            this.lblImportCSV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(30, 60);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(270, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(310, 55);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(90, 32);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "📁 Caută";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnExecuteImport
            // 
            this.btnExecuteImport.Location = new System.Drawing.Point(30, 110);
            this.btnExecuteImport.Name = "btnExecuteImport";
            this.btnExecuteImport.Size = new System.Drawing.Size(250, 40);
            this.btnExecuteImport.TabIndex = 3;
            this.btnExecuteImport.Text = "📥 Importă Fișierul";
            this.btnExecuteImport.UseVisualStyleBackColor = true;
            this.btnExecuteImport.Click += new System.EventHandler(this.btnExecuteImport_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(290, 110);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(110, 40);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Înapoi";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 170);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnExecuteImport);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblImportCSV);
            this.Name = "ImportForm";
            this.Text = "Import CSV — SalesManagementSystem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblImportCSV;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnExecuteImport;
        private System.Windows.Forms.Button btnBack;
    }
}