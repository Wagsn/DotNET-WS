using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 加载函数
    /// </summary>
    public class LoadData : VarData
    {
        /// <summary>
        /// 
        /// </summary>
        public LoadData()
        {
            Name = "load";
            Kind = "Function";
            //IsUnit = true;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override VarData Run(VarData[] args)
        {
            if (args.Count() == 0)
            {
                Console.WriteLine("System Info: Non argumants for load.");
                return new NoneData();
            }
            var filePath = GetData(args.First()).ToString();

            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine($"System Info: file does not exist. {filePath}");
                return new NoneData();
            }
            var str = System.IO.File.ReadAllText(filePath);
            return new StringData
            {
                Data = str,
                Raw = str
            };
        }
    }
}
