using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS.Editor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //MyConsole console = new MyConsole();
            //console.WriteLine("控制台");
            //NativeMethods.AllocConsole();
            //NativeMethods.AttachConsole(NativeMethods.ATTACH_PARENT_PROCESS);
            //NativeMethods.OpenConsole();
            //Console.WriteLine("This is a test");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
            //Application.Run(new MainForm());
        }
    }
}
