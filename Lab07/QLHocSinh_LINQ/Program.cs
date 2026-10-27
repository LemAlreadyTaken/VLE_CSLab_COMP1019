using System;
using System.Windows.Forms;

namespace QLHocSinh_LINQ
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmQLHocSinh());
        }
    }
}
