using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Log
{
    /// <summary>
    /// 日志管理配置文件
    /// </summary>
    public class LogConfig
    {
        public LogConfig() { }

        /// <summary>
        /// 文件模板
        /// </summary>
        public string LogFileTemplate { get; set; }

        /// <summary>
        /// 日志模板（[{LogEntity.LogTime}] [{LogEntity.LogLevel}] [{LogEntity.LogName}] {LogEntity.Message}）
        /// </summary>
        public string LogContentTemplate { get; set; }

        /// <summary>
        /// 是否异步
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        /// 日志根路径（默认./log）
        /// </summary>
        public string LogBaseDir { get; set; }

        /// <summary>
        /// 默认日志层级
        /// </summary>
        public LogLevels LogLevels { get; set; }

        /// <summary>
        /// 档案文件最大限制
        /// </summary>
        public int MaxArchiveFiles { get; set; }

        /// <summary>
        /// 文件最大限制
        /// </summary>
        public string MaxFileSize { get; set; }

        /// <summary>
        /// 档案文件模板
        /// </summary>
        public string ArchiveFileTemplate { get; set; }

        /// <summary>
        /// 档案文件路径
        /// </summary>
        public string ArchiveBaseDir { get; set; }

        /// <summary>
        /// 档案文件写入权限码
        /// </summary>
        public string ArchiveWriteMode { get; set; }

        /// <summary>
        /// 档案文件
        /// </summary>
        public string ArchiveEvery { get; set; }

        /// <summary>
        /// 档案文件是否压缩
        /// </summary>
        public bool? ZipArchiveFile { get; set; }

        /// <summary>
        /// 日志根路径
        /// </summary>
        public string LogRoot { get; set; }

        /// <summary>
        /// 错误日志输出路径
        /// </summary>
        public string ErrRoot { get; set; }

        /// <summary>
        /// 调试日志输出路径
        /// </summary>
        public string DebugRoot { get; set; }

        /// <summary>
        /// 时间格式
        /// </summary>
        public string TimeFormat { get; set; }
    }
}
