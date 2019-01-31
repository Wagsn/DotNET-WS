using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 字面量
    /// </summary>
    public class LiteralData : VarData
    {
        public LiteralData()
        {
            Data = "Literal";
            Name = "Literal";
            Kind = "Literal";
            Raw = "Literal";
            IsUnit = true;
        }

        public override VarData Get()
        {
            return this;
        }

        public override void Set(VarData[] args)
        {
            Console.WriteLine("Literal can not be set value");
        }

        public override VarData Run(VarData[] args)
        {
            Console.WriteLine("Literal can not be set value");
            return this;
        }
    }
}
