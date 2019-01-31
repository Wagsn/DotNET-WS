using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 字符串字面量
    /// </summary>
    public class StringData : LiteralData
    {
        public StringData(string raw): this()
        {
            Raw = raw;
            Data = raw.Trim('\'');
        }

        public StringData()
        {
            Name = "String";
            Kind = "String";
            Data = "None";
            Raw = "String";
            //Type = typeof(StringData);
        }

        public override void Set(VarData[] args)
        {
            if (args.Length >= 1)
            {
                if(args.Length > 1) Console.WriteLine("System Info: 参数过多");
                Data = args[0].Data;
            }
        }

        public override VarData Get()
        {
            return this;
        }
    }
}
