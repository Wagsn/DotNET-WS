#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：ShellContextInjectionCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/23 10:57:55
* 更新时间 ：2018/11/23 10:57:55
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using WS.Shell.CmdUnit;

namespace WS.Shell
{
    public static class ShellContextExtensions
    {
        /// <summary>
        /// 添加Cmds，此为ShellContext的扩展方法（目的是较小的改动ShellContext）
        /// </summary>
        public static void AddCmds(this ShellContext context)
        {
            ShellContext.CmdManager manager = context.cmdManager;
            manager.Add(new ToJsonCmd(context));
            manager.Add(new HelpCmd(context));
            manager.Add(new NowCmd(context));
            manager.Add(new VarCmd(context));
            manager.Add(new AddCmd(context));
            manager.Add(new EchoCmd(context));
            manager.Add(new IdCmd(context));
            manager.Add(new TestCmd(context));
            manager.Add(new ScriptCmd(context));
        }
    }
}
