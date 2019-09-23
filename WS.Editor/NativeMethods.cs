using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WS.Editor
{
    /// <summary>
    /// 静态本地方法
    /// </summary>
    public static class NativeMethods
    {
        /// <summary>
        /// 启动控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        /// <summary>
        /// 释放控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        //[DllImport("kernel32.dll", EntryPoint = "FreeConsole")]
        public static extern bool FreeConsole();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwProcessId"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool AttachConsole(int dwProcessId);

        /// <summary>
        /// 通过窗口名称寻找窗口ID
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll ", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 取出窗口运行的菜单
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]
        public extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="nPos"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]
        //extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);
        public extern static int RemoveMenu(IntPtr hMenu, uint nPos, uint flags);

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern bool SetConsoleTitle(string strMessage);

        /// <summary>
        /// 设置父窗口
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="hWndNewParent"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// 打开控制台并禁用关闭菜单
        /// TODO 控制台优化，可以输入，可以对控制台精准控制
        /// </summary>
        public static void OpenConsole()
        {
            AllocConsole();
            // 根据控制台标题找控制台
            int WINDOW_HANDLER = FindWindow(null, Console.Title);
            SetConsoleTitle($"Editor[{MainWindow.Id}]");
            // 找关闭按钮 次代码和B下的代码是禁用打开窗口的关闭按钮（同时也会关闭程序）         
            IntPtr CLOSE_MENU = GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);
            uint SC_CLOSE = 0xF060;
            // 关闭按钮禁用 （A和B可根据情况选用）         
            RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);

            Console.WriteLine("控制台已开启");
        }

        /// <summary>
        /// 释放控制台方法（可释放控制台）
        /// </summary>
        public static void CloseConsole()
        {
            FreeConsole();
            Console.WriteLine("控制台已关闭");
        }

        public static void OpenCmd()
        {
            var cmd = Process.Start("cmd");
            SpinWait.SpinUntil(() => cmd.MainWindowHandle != (IntPtr)0);
            //SetParent(cmd.MainWindowHandle, Handle);
        }

    }
}
