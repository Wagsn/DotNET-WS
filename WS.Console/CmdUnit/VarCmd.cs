#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：VarCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 1:11:49
* 更新时间 ：2018/11/22 1:11:49
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 变量声明，以后用抽象语法树（AST）来解决吧
    /// </summary>
    public class VarCmd : CmdUnitBase
    {
        public VarCmd(ShellContext context) : base(context) { }

        public override void Init()
        {
            Name = "var";
            Desc = "变量声明<暂时只有Int声明>";
            Usage = "var num:Int32=32141";
        }

        /// <summary>
        /// 执行
        /// 参数; "num:Int32=32141"  // 单变量定义
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            // 单变量定义
            // TODO 正则验证
            // 切割
            string[] vars = arg.Split("=");
            // 声明部分
            string declare = vars[0];
            // 值部分 字符串形式
            string valueRaw = vars[1];

            string name = null;
            string type = null;
            int value = 0;
            if (declare.IndexOf(":") > 0)
            {
                var declares = declare.Split(":");
                name = declares[0];  // 命名规则验证
                type = declares[1];  // 类型校验 
            }
            else
            {
                name = declare;  // 命名规则验证
            }
            // 值解析
            try
            {
                VarEntry entry;
                if (AppContext.VarTable.ContainsKey(name))
                {
                    entry = AppContext.VarTable[name];
                    entry.raw = arg;
                }
                else
                {
                    entry = new VarEntry
                    {
                        name = name,
                        raw = arg,
                        value = new VarValue()
                    };
                    AppContext.VarTable.Add(name, entry);
                }
                // 匹配数字
                if (MatchNumber(valueRaw, out double dbval))
                {
                    entry.value.value = dbval;
                    entry.value.type = typeof(double);
                    entry.value.kind = "number";
                }
                // 匹配布尔
                else if (MatchBoolean(valueRaw, out bool blval))
                {
                    entry.value.value = blval;
                    entry.value.type = typeof(bool);
                    entry.value.kind = "boolean";
                }
                // 其它作字符串处理
                else
                {
                    entry.value.value = valueRaw;
                    entry.value.type = typeof(string);
                    entry.value.kind = "string";
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("字符串转换成数字失败: " + e);
                return -1;
            }
        }

        /// <summary>
        /// 匹配布尔类型
        /// </summary>
        /// <param name="boolstr"></param>
        /// <param name="boolean"></param>
        /// <returns></returns>
        private bool MatchBoolean(string boolstr, out bool boolean)
        {
            return bool.TryParse(boolstr, out boolean);
        }

        /// <summary>
        /// 判断是否为数字，（为了广泛使用仅支持double）
        /// </summary>
        /// <param name="numstr"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private bool MatchNumber(string numstr, out double num)
        {
            return double.TryParse(numstr, out num);
        }
    }
}
