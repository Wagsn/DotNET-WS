#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：ToJsonCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 10:39:13
* 更新时间 ：2018/11/22 10:39:13
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using WS.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// ToJson命令 将对象转换成JSON字符串输出到控制台
    /// </summary>
    public class ToJsonCmd : CmdUnitBase
    {
        public ToJsonCmd(ShellContext context) : base(context) { }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            Name = "tojson";
            Desc = "将对象转换成字符串。（暂时不能保证文本显示与输入的统一）";  // 控制台不支持emoji😟（UTF-32）符号，可能因为系统默认UTF-16，// tojson需要将argument参数解析成变量组，在变量表中寻找变量的实际值，然后传递给tojson函数，这里需要将tojson封装成一个函数。
            Usage = "tojson [argument|var_name]";
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="arg">参数</param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            Console.WriteLine("raw: " + JsonUtil.ToJson(arg));
            // 在变量表中查询arg所含有的参数
            // 解析arg为参数组，在context的变量表中找出这些变量，再将变量转换成JSON字符串输出的到控制台，如果找不到
            string normal = WS.Text.Format.NormalSpace(arg);
            string[] words = normal.Split(" ");
            Console.Write("out: ");
            // 这里最好和原始输入对应起来，只是将变量替换
            for (int i = 0; i < words.Length; i++)
            {
                if (AppContext.VarTable.ContainsKey(words[i]))
                {
                    Console.Write(AppContext.VarTable[words[i]].Data.Data);
                }
                else
                {
                    Console.Write(words[i]);
                }
                if (i < words.Length)
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
            return 0;
        }
    }
}
