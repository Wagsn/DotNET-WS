#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.CmdUnit
* 项目描述 ：
* 类 名 称 ：NowCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 10:35:14
* 更新时间 ：2018/11/22 10:35:14
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// 现在的时间
    /// </summary>
    public class NowCmd : CmdUnitBase
    {
        public NowCmd(ShellContext context) : base(context) { }

        public override void Init()
        {
            Name = "now";
            Desc = "显示当前时间";
            Usage = "now";  // TODO now <format>  // now "yy-MM-dd HH:mm:ss.SSS"
        }

        public override int Excute(string arg)
        {
            Console.WriteLine(DateTime.Now.ToString(Define.Format.Time));
            return 0;
        }
    }
}
