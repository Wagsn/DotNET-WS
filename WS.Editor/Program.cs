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
        static void Main(string[] args)
        {
            //NativeMethods.OpenConsole();
            //NativeMethods.CloseConsole();
            if (args!=null && args.Length > 0)
            {
                // 注：在这里打开控制台，调试模式下后面调用的Console输出都是输出在控制台上
                NativeMethods.OpenConsole();
                Console.WriteLine($"控制台输入命令：{string.Join(", ", args)}");
                //MessageBox.Show($"控制台输入命令：{string.Join(", ", args)}");
            }
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
