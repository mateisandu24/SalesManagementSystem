using SalesManagementSystem.Utils;

namespace SalesManagementSystem.Forms
{
    partial class CartForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // dgvCart
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.Location = new System.Drawing.Point(0, 0);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.Size = new System.Drawing.Size(800, 380);
            this.dgvCart.TabIndex = 0;
            // pnlBottom
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 380);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(800, 70);
            this.pnlBottom.TabIndex = 3;
            this.pnlBottom.Controls.Add(this.lblTotal);
            this.pnlBottom.Controls.Add(this.btnCheckout);
            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(15, 20);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(50, 21);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Total:";
            // btnCheckout
            this.btnCheckout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckout.Location = new System.Drawing.Point(580, 10);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(200, 45);
            this.btnCheckout.TabIndex = 2;
            this.btnCheckout.Text = "Finalizează Comanda";
            this.btnCheckout.UseVisualStyleBackColor = true;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // CartForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.pnlBottom);
            this.Name = "CartForm";
            this.Text = "Coșul Meu — SalesManagementSystem";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Panel pnlBottom;
    }
}