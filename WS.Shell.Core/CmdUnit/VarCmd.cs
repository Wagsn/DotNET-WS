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
using System.Linq;
using System.Text;
using WS.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// 变量声明，以后用抽象语法树（AST）来解决吧
    /// </summary>
    public class VarCmd : CmdUnitBase
    {
        public VarCmd(ShellContext context) : base(context) { }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            Name = "var";
            Desc = "变量声明<暂时有Number，Boolean，String类型>";
            Usage = "var num:Number=32141";
        }

        /// <summary>
        /// 执行: TODO 支持对象创建
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            // 单变量定义
            // TODO 正则验证
            // 切割
            List<string> vars = arg.Split('=').ToList();
            // 声明部分
            string declare = vars[0];
            // 值部分 字符串形式
            string valueRaw = vars[1];
            
            string varName = null;
            string varType = null;
            if (declare.IndexOf(":") > 0)
            {
                var declares = declare.Split(':');
                varName = declares[0].NormalSpace();  // 命名规则验证
                varType = declares[1].NormalSpace();  // 类型校验 
            }
            else
            {
                varName = declare.NormalSpace();  // 命名规则验证
            }
            // 值解析
            try
            {
                VarEntry entry;
                if (AppContext.VarTable.ContainsKey(varName))
                {
                    entry = AppContext.VarTable[varName];
                    entry.Raw = arg;
                }
                else
                {
                    entry = new VarEntry
                    {
                        Name = varName,
                        Raw = arg,
                        Data = new VarData()
                    };
                    AppContext.VarTable.Add(varName, entry);
                }
                // 匹配数字
                if (MatchNumber(valueRaw, out double dbval))
                {
                    entry.Data.Data = dbval;
                    //entry.Data.Type = typeof(double);
                    entry.Data.Kind = "number";
                }
                // 匹配布尔
                else if (MatchBoolean(valueRaw, out bool blval))
                {
                    entry.Data.Data = blval;
                    //entry.Data.Type = typeof(bool);
                    entry.Data.Kind = "boolean";
                }
                // 其它作字符串处理
                else
                {
                    entry.Data.Data = valueRaw;
                    //entry.Data.Type = typeof(string);
                    entry.Data.Kind = "string";
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
