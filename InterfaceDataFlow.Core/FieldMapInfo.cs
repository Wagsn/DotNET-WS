using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceDataFlow
{
    /// <summary>
    /// 字段映射信息
    /// 将响应数据映射到请求数据中
    /// </summary>
    public class FieldMapInfo
    {
        /// <summary>
        /// 占位符
        /// </summary>
        public string FromName { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string FromLocation { get; set; }
        /// <summary>
        /// 占位符
        /// </summary>
        public string ToName { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string ToLocation { get; set; }
    }
}
