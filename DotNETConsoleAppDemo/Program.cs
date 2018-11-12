using System;
using WS.Core.Attributes;

namespace DotNETConsoleAppDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("利用特性进行参数校验");

            Logger.log(new string("sssssssssss"));
            Logger.log(null);

            Console.ReadKey();
        }
    }

    public static class Logger {

        /// <summary>
        /// 注释未起作用，需要依赖注入
        /// </summary>
        /// <param name="obj"></param>
        public static void log([NotNull]object obj)
        {
            Console.WriteLine(obj);
            return;
        }
    }
}
