using System;
using System.Windows.Forms;

namespace THTMS_GUI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // .NET Framework 4.x 无 SetHighDpiMode，保持兼容
            Application.Run(new Form1());
        }
    }
}
