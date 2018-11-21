using System;
using System.Collections.Generic;

using WS.Core.Helpers;

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
                return App.New(args).Run();
            }
            else
            {
                return App.New().Run();
            }
        }
    }
}
