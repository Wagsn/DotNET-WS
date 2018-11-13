using System;

namespace WS.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Wagsn Shell 的应用上下文
            ShellContext AppContext = new ShellContext();
            Console.Write($"Wagsn Shell\r\n版权所有 (C) Wagsn。保留所有权利。\r\n\r\nWS {System.IO.Directory.GetCurrentDirectory()}> ");
            // 一个简单的控制台循环
            string nextLine = "";
            //bool hasOutput = true;
            while (true)
            {
                nextLine = Console.ReadLine();
                switch (nextLine)
                {
                    case "":
                        //hasOutput = false;
                        break;
                    case "exit":
                        return;
                    case "clear":
                        Console.Clear();
                        //hasOutput = false;
                        break;
                    case "clearlast":
                        Console.SetCursorPosition(0, Console.CursorSize - 1);
                        break;
                    case "now":
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFK"));
                        break;
                    case "help":
                        Console.WriteLine("[command]\t[decription]\r\nhelp\t\t帮助\r\nnow\t\t当前时间\r\nclear\t\t清除屏幕\r\nexit\t\t退出 Wagsn Shell\r\n");
                        break;
                    default:
                        Console.WriteLine("找不到关于 \"" + nextLine + "\" 的命令\r\n");
                        //hasOutput = false;
                        break;
                }
                //if (hasOutput) Console.WriteLine("WS < " + nextLine);
                //hasOutput = true;
                Console.Write($"WS {System.IO.Directory.GetCurrentDirectory()}> ");
            }
        }

        /// <summary>
        /// 程序启动时使用
        /// </summary>
        static void Startup()
        {

        }

        static void MainHadle(string line)
        {
            switch (line)
            {
                case "clear":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("找不到关于 \"" + line + "\"的命令");
                    break;
            }
        }
    }

    /// <summary>
    /// Wagsn Shell Context
    /// </summary>
    class ShellContext
    {
        public string CurrentDirectory { get; set; }
    }
}
