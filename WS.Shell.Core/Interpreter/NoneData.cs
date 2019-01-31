using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 空
    /// </summary>
    public class NoneData : VarData
    {
        public NoneData()
        {
            Data = "None";
        }

        public override VarData Get()
        {
            Console.WriteLine("None can not be set value");
            return new NoneData();
        }

        public override void Set(VarData[] args)
        {
            Console.WriteLine("None can not be set value");
        }

        public override VarData Run(VarData[] args)
        {
            Console.WriteLine("None can not be set value");
            return new NoneData();
        }
    }
}
