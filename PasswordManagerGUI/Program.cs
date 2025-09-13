using System;
using System.Windows.Forms;
using SQLitePCL;  // for Batteries_V2.Init()

namespace PasswordManagerGUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Initialize the native SQLite engine required by Microsoft.Data.Sqlite
            try
            {
                Batteries_V2.Init();   // preferred for modern bundle versions
            }
            catch
            {
                // Fallback for older bundle versions
                Batteries.Init();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
