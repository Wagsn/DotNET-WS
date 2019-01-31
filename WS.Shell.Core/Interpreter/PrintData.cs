using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WS.Text;

namespace WS.Shell
{
    /// <summary>
    /// 打印对象
    /// </summary>
    public class PrintData : VarData
    {
        /// <summary>
        /// 
        /// </summary>
        public PrintData()
        {
            Name = "print";
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
                Console.WriteLine("System Info: Non argumants for print.");
            }
            var datas = args.Select(vd => GetData(vd));
            foreach(var d in datas)
            {
                Console.Write(d);
                Console.Write(" ");
            }
            Console.WriteLine();

            //// 这里需要重新解析一下
            //var show = JsonUtil.ToJson(datas);
            //Console.WriteLine(show);
            return new NoneData();
        }
    }
}
