#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：Wagsn Shell
* 类 名 称 ：App
* 类 描 述 ：用于描述一个运行Shell实体
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 11:19:24
* 更新时间 ：2018/11/21 11:19:24
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using WS.Text;

namespace WS.Shell
{
    /// <summary>
    /// TODO 在New方法调用的时候创建一个新的线程以及控制台窗口，在静态成员中用ConsoleManager来管理，或者直接新建一个新的应用程序
    /// </summary>
    public class App
    {
        /// <summary>
        /// 新建一个Shell
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static App New(string[] args)
        {
            Console.WriteLine($"args:\r\n{JsonUtil.ToJson(args)}");
            return new App();
        }

        public static App New()
        {
            return new App();
        }

        /// <summary>
        /// 程序启动时使用
        /// </summary>
        public static App Startup(ShellConfig config)
        {
            //try
            //{
            //    ProcessStartInfo startInfo = new ProcessStartInfo();
            //    startInfo.FileName = "WindowsFormsApplication1.exe"; //启动的应用程序名称
            //    startInfo.Arguments = "我是由控制台程序传过来的参数，如果传多个参数用空格隔开" + " 第二个参数";
            //    startInfo.WindowStyle = ProcessWindowStyle.Normal;
            //    Process.Start(startInfo);

            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            return New();
        }

        /// <summary>
        /// 运行，这个函数的返回值将作为主函数的返回值
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            // Wagsn Shell 的应用上下文初始化
            ShellContext AppContext = new ShellContext();
            // 打印版本信息
            Console.Write($"{AppContext.HelloInfo}\r\n\r\nWS {AppContext.CurrentDirectory}> ");

            // 一些中间临时变量
            string nextLine = null;
            string[] nextWords = null;
            // 命令
            string cmd = null;
            // 参数：可能是多个，交由处理函数自行切割
            string arg = null;

            // 一个简单的控制台循环
            while (true)
            {
                nextLine = Console.ReadLine().Trim();
                nextWords = nextLine.Split(" ");
                cmd = nextWords[0];
                arg = nextLine.Substring(cmd.Length).Trim();
                try
                {
                    switch (cmd)
                    {
                        case "":
                            break;
                        case "exit":
                            return 0;
                        case "clearlast":
                            Console.SetCursorPosition(0, Console.CursorSize - 1);
                            break;
                        case "test_split":
                            Console.WriteLine("String.Split \"hello world\" with \" \": " + JsonUtil.ToJson("hello world".Split(" "))); // ["hello","world"]
                            Console.WriteLine("String.Split \"\" with \" \": " + JsonUtil.ToJson("".Split(" ")));  // [""]
                            break;
                        case "exception":
                            throw new Exception("test exception");
                        default:
                            AppContext.cmdManager.Run(cmd, arg);
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"命令执行失败:\r\n{e}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }
                Console.Write($"WS {AppContext.CurrentDirectory}> ");
            }
        }
    }
}
