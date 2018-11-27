using System;
using System.Collections.Generic;
using WS.Log;
using WS.Text;

namespace WS.Shell
{
    /// <summary>
    /// 程序
    /// </summary>
    class Program
    {
        public ILogger Logger = LoggerManager.GetLogger();

        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            if (args != null && args.Length != 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                try
                {
                    // 尝试将错误写入日志  // 写一个日志工具
                    IO.File.WriteAllText("./log/err/" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_FFFFFF") + ".log", e.ToString());
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2);
                    Console.ReadKey();
                }
                return -1;
            }
            else
            {
                try
                {
                    return App.New().Run();
                }catch(Exception e)
                {
                    try
                    {
                        // 尝试将错误写入日志  // 写一个日志工具
                        IO.File.WriteAllText("./log/err/" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_FFFFFF") + ".log", e.ToString());
                        Console.WriteLine(e);
                        Console.ReadKey();
                    }
                    catch (Exception e2)
                    {
                        Console.WriteLine(e2);
                        Console.ReadKey();
                    }
                    return -1;
                }
            }
        }
    }
}
