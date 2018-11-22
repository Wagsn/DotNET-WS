#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.CmdUnit
* 项目描述 ：
* 类 名 称 ：ViewCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 10:40:21
* 更新时间 ：2018/11/22 10:40:21
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// 查看文本文件，将文本输出到控制台
    /// </summary>
    public class ViewCmd : CmdUnitBase
    {
        public ViewCmd(ShellContext context) : base(context) { }

        public override void Init()
        {
            Name = "view";
            Desc = "在控制台上显示文本文件的内容";
            Usage = "view <file path>";
        }

        public override int Excute(string arg)
        {
            throw new NotImplementedException();
        }
    }
}
