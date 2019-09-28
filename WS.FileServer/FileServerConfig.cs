using System;
using System.Collections.Generic;
using System.Text;

namespace WS.FileServer
{
    /// <summary>
    /// 文件配置
    /// </summary>
    public class FileServerConfig
    {
        public PathItem Root { get; set; }
        public List<PathItem> PathList { get; set; }
    }

    /// <summary>
    /// 文件项
    /// </summary>
    public class PathItem
    {
        /// <summary>
        /// 本机物理地址（F:/File）
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// 映射的URL相对路径(file)
        /// </summary>
        public string Url { get; set; }
    }
}
