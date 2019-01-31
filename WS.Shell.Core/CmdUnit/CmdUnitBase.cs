#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.CmdUnit
* 项目描述 ：
* 类 名 称 ：CmdUnitBase
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 10:37:24
* 更新时间 ：2018/11/22 10:37:24
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// 命令单元的基类，命令必须依赖于上下文
    /// </summary>
    public abstract class CmdUnitBase : ICmdUnit
    {
        public string Desc { get; set; }
        public string Name { get; set; }
        public string Usage { get; set; }
        public ShellContext AppContext { get; set; }

        /// <summary>
        /// 初始化：定义 Name Desc Usage 之类的东西
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="arg">命令所需的参数</param>
        /// <returns></returns>
        public abstract int Excute(string arg);
        
        /// <summary>
        /// 上下文初始化，命令都需要上下文
        /// </summary>
        /// <param name="context">应用上下文</param>
        public void Init(ShellContext context)
        {
            AppContext = context;
            Init();
        }

        public CmdUnitBase(ShellContext context)
        {
            Init(context);
        }
    }
}
