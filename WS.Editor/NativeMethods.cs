﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);

        public const int ATTACH_PARENT_PROCESS = -1;

        public static void OpenConsole()
        {
            AllocConsole();
            //根据控制台标题找控制台
            //int WINDOW_HANDLER = FindWindow(null, Console.Title);
            //A找关闭按钮 次代码和B下的代码是禁用打开窗口的关闭按钮（同时也会关闭程序）         
            //IntPtr CLOSE_MENU = GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);
            //int SC_CLOSE = 0xF060;
            //B关闭按钮禁用 （A和B可根据情况选用）         
            //RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);

            Console.WriteLine("程序已启动");
        }

        ///
        /// 释放控制台方法（可释放控制台）
        ///
        public static void CloseConsole()
        {
            FreeConsole();
        }

    }
}
