using System;
using System.Collections.Generic;

using WS.Core.Text;

namespace WS.Shell
{
    /// <summary>
    /// 程序
    /// </summary>
    class Program
    {
        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            try
            {
                if (args != null && args.Length != 0 && !string.IsNullOrWhiteSpace(args[0]))
                {
                    return App.New(args).Run();
                }
                else
                {
                    return App.New().Run();
                }
            }
            catch(Exception e)
            {
                try
                {
                    // 尝试将错误写入日志  // 写一个日志工具
                    Core.IO.File.WriteAllText("./log/err/" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_FFFFFF") + ".log", e.ToString());
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
                catch(Exception e2)
                {
                    Console.WriteLine(e2);
                    Console.ReadKey();
                }
                return -1;
            }
        }
    }
}
