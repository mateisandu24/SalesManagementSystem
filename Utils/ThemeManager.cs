using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesManagementSystem.Utils
{
    public static class ThemeManager
    {
        // Colors
        public static Color BackgroundColor = Color.FromArgb(245, 247, 250);
        public static Color PrimaryColor = Color.FromArgb(67, 97, 238);
        public static Color PrimaryColorHover = Color.FromArgb(58, 86, 212);
        public static Color TextColor = Color.FromArgb(43, 45, 66);
        public static Color TextLight = Color.FromArgb(141, 153, 174);
        public static Color ControlBackground = Color.White;
        
        // Fonts
        public static Font MainFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static Font HeaderFont = new Font("Segoe UI", 16F, FontStyle.Bold);
        public static Font ButtonFont = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        public static Font LabelFont = new Font("Segoe UI", 10F, FontStyle.Regular);

        public static void ApplyTheme(Form form)
        {
            form.BackColor = BackgroundColor;
            form.Font = MainFont;
            form.ForeColor = TextColor;
            form.StartPosition = FormStartPosition.CenterScreen;

            ApplyToControls(form.Controls);
        }

        private static void ApplyToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.Font = ButtonFont;
                    btn.Cursor = Cursors.Hand;
                    btn.Height = Math.Max(btn.Height, 35);
                }
                else if (control is Label lbl)
                {
                    lbl.Font = LabelFont;
                    lbl.ForeColor = TextColor;
                }
                else if (control is TextBox txt)
                {
                    txt.Font = MainFont;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = ControlBackground;
                  
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = ControlBackground;
                    dgv.BorderStyle = BorderStyle.None;
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.GridColor = Color.FromArgb(230, 230, 230);
                    dgv.RowHeadersVisible = false;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
                    headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    headerStyle.BackColor = Color.FromArgb(41, 50, 65);
                    headerStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
                    headerStyle.ForeColor = Color.White;
                    headerStyle.SelectionBackColor = Color.FromArgb(41, 50, 65);
                    dgv.ColumnHeadersDefaultCellStyle = headerStyle;
                    dgv.ColumnHeadersHeight = 40;

                    DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
                    rowStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    rowStyle.BackColor = ControlBackground;
                    rowStyle.Font = MainFont;
                    rowStyle.ForeColor = TextColor;
                    rowStyle.Padding = new Padding(5);
                    rowStyle.SelectionBackColor = Color.FromArgb(224, 238, 255);
                    rowStyle.SelectionForeColor = Color.FromArgb(30, 30, 30);
                    dgv.DefaultCellStyle = rowStyle;
                    dgv.RowTemplate.Height = 35;
                }
                
                if (control.HasChildren)
                {
                    ApplyToControls(control.Controls);
                }
            }
        }
    }
}
