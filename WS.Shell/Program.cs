using System;
using System.Collections.Generic;
using WS.Text;

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
            if (args != null && args.Length != 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                try
                {
                    return App.New(args).Run();
                }
                catch(Exception e)
                {
                    try
                    {
                        Console.WriteLine(e);
                        IO.File.WriteAllText("./log/err/" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_FFFFFF") + ".log", e.ToString());
                        Console.ReadKey();
                    }
                    catch (Exception e2)
                    {
                        Console.WriteLine(e2);
                        Console.ReadKey();
                    }
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
