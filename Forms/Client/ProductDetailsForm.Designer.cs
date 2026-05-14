namespace SalesManagementSystem.Forms.Client
{
    partial class ProductDetailsForm
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
            this.pbProductImage = new System.Windows.Forms.PictureBox();
            this.lblProductname = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnAction = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnBuyNow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProductImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbProductImage
            // 
            this.pbProductImage.Location = new System.Drawing.Point(12, 12);
            this.pbProductImage.Name = "pbProductImage";
            this.pbProductImage.Size = new System.Drawing.Size(476, 220);
            this.pbProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbProductImage.TabIndex = 0;
            this.pbProductImage.TabStop = false;
            // 
            // lblProductname
            // 
            this.lblProductname.AutoSize = true;
            this.lblProductname.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductname.Location = new System.Drawing.Point(16, 240);
            this.lblProductname.MaximumSize = new System.Drawing.Size(470, 0);
            this.lblProductname.Name = "lblProductname";
            this.lblProductname.Size = new System.Drawing.Size(51, 21);
            this.lblProductname.TabIndex = 1;
            this.lblProductname.Text = "label1";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.lblPrice.Location = new System.Drawing.Point(16, 270);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(46, 20);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "label2";
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Location = new System.Drawing.Point(16, 295);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(41, 13);
            this.lblBrand.TabIndex = 3;
            this.lblBrand.Text = "label3";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(20, 380);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(460, 60);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(230, 475);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(120, 35);
            this.btnAction.TabIndex = 5;
            this.btnAction.Text = "Adaugă în Coș";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(360, 475);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(120, 35);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Înapoi";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ProductDetailsForm
            // 
                        // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblStock.Location = new System.Drawing.Point(20, 350);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(100, 19);
            this.lblStock.TabIndex = 7;
            this.lblStock.Text = "Stoc disponibil: ";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuantity.Location = new System.Drawing.Point(20, 440);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(68, 19);
            this.lblQuantity.TabIndex = 8;
            this.lblQuantity.Text = "Cantitate:";
            // 
            // nudQuantity
            // 
            this.nudQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudQuantity.Location = new System.Drawing.Point(100, 438);
            this.nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(70, 25);
            this.nudQuantity.TabIndex = 9;
            this.nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnBuyNow
            // 
            this.btnBuyNow.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnBuyNow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuyNow.FlatAppearance.BorderSize = 0;
            this.btnBuyNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuyNow.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuyNow.ForeColor = System.Drawing.Color.White;
            this.btnBuyNow.Location = new System.Drawing.Point(20, 475);
            this.btnBuyNow.Name = "btnBuyNow";
            this.btnBuyNow.Size = new System.Drawing.Size(160, 35);
            this.btnBuyNow.TabIndex = 10;
            this.btnBuyNow.Text = "🛒 Cumpără Acum";
            this.btnBuyNow.UseVisualStyleBackColor = false;
            this.btnBuyNow.Click += new System.EventHandler(this.BtnBuyNow_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 535);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        this.Controls.Add(this.btnBuyNow);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblProductname);
            this.Controls.Add(this.pbProductImage);
            this.Name = "ProductDetailsForm";
            this.Text = "Detalii Produs — SalesManagementSystem";
                        ((System.ComponentModel.ISupportInitialize)(this.pbProductImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            
            // Theme Settings
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.ForeColor = System.Drawing.Color.FromArgb(43, 45, 66);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.btnAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAction.FlatAppearance.BorderSize = 0;
            this.btnAction.BackColor = System.Drawing.Color.FromArgb(67, 97, 238);
            this.btnAction.ForeColor = System.Drawing.Color.White;
            this.btnAction.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(67, 97, 238);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuyNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuyNow.FlatAppearance.BorderSize = 0;
            this.btnBuyNow.BackColor = System.Drawing.Color.FromArgb(67, 97, 238);
            this.btnBuyNow.ForeColor = System.Drawing.Color.White;
            this.btnBuyNow.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnBuyNow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductname.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblProductname.ForeColor = System.Drawing.Color.FromArgb(43, 45, 66);
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(43, 45, 66);
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(43, 45, 66);
            this.lblStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblStock.ForeColor = System.Drawing.Color.FromArgb(43, 45, 66);
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(43, 45, 66);

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbProductImage;
        private System.Windows.Forms.Label lblProductname;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Button btnBuyNow;
    }
}