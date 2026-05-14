using System;
using System.Windows.Forms;
using SalesManagementSystem.Forms.InOut;
namespace SalesManagementSystem
{
    internal static class Program
    {
        
        
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
