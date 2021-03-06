﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.CmdUnit
* 项目描述 ：
* 类 名 称 ：IdCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 10:40:47
* 更新时间 ：2018/11/22 10:40:47
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// 显示当前启动ID
    /// </summary>
    public class IdCmd : CmdUnitBase
    {
        public IdCmd(ShellContext context) : base(context) { }

        public override int Excute(string arg)
        {
            if (arg == "app")
            {
                Console.WriteLine(ShellContext.AppId);
            }
            else
            {
                Console.WriteLine(AppContext.StartId);
            }
            return 0;
        }

        public override void Init()
        {
            Name = "id";
            Desc = "显示当前启动ID";
            Usage = "id";
        }
    }
}
