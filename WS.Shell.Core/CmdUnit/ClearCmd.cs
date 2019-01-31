#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.CmdUnit
* 项目描述 ：
* 类 名 称 ：ClearCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 10:41:31
* 更新时间 ：2018/11/22 10:41:31
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// 清除控制台的显示
    /// </summary>
    public class ClearCmd : CmdUnitBase
    {
        public ClearCmd(ShellContext context) : base(context) { }

        public override int Excute(string arg)
        {
            Console.Clear();
            return 0;
        }

        public override void Init()
        {
            Name = "clear";
            Desc = "清除控制台显示";
            Usage = "clear";
        }
    }
}
