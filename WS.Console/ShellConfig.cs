#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：ShellConfig
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 11:18:50
* 更新时间 ：2018/11/21 11:18:50
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// Wagsn Shell 配置，必须要可序列化
    /// </summary>
    public class ShellConfig
    {
        /// <summary>
        /// Wagsn Shell 应用上下文
        /// </summary>
        public ShellContext Context { get; set; }
    }
}
