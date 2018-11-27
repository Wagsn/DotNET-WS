#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Core.Log
* 项目描述 ：
* 类 名 称 ：LoggerConfig
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Core.Log
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/23 9:14:28
* 更新时间 ：2018/11/23 9:14:28
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Log
{
    /// <summary>
    /// 单独一个日志器的配置
    /// LoogerName
    /// LoggerRoot
    /// FileFormat
    /// TimeFormat
    /// </summary>
    public class LoggerConfig
    {
        /// <summary>
        /// 日志器名称
        /// </summary>
        public string LoggerName { get; set; }
        
        /// <summary>
        /// 日志文件根路径（./log/loggerName）
        /// </summary>
        public string LoggerRoot { get; set; }

        /// <summary>
        /// 文件名格式（yyyy-MM-dd）
        /// </summary>
        public string FileFormat { get; set; }

        /// <summary>
        /// 时间格式（2018-11-23 09:53:12.154451+8:00）
        /// </summary>
        public string TimeFormat { get; set; }
    }
}
