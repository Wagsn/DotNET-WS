#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.CmdUnit
* 项目描述 ：
* 类 名 称 ：EchoCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 10:39:43
* 更新时间 ：2018/11/22 10:39:43
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// 控制台输出
    /// </summary>
    public class EchoCmd : CmdUnitBase
    {
        /// <summary>
        /// 用于输出变量集中的数据
        /// </summary>
        /// <param name="context"></param>
        public EchoCmd(ShellContext context) : base(context) { }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            Console.WriteLine(arg);
            return 0;
        }

        public override void Init()
        {
            Name = "echo";
            Desc = "输出到控制台";
            Usage = "ccho [string]";
        }
    }
}
