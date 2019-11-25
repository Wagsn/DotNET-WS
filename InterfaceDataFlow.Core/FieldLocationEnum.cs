using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceDataFlow
{
    /// <summary>
    /// 字段位置枚举
    /// </summary>
    public enum FieldLocationEnum
    {
        /// <summary>
        /// 请求或响应体
        /// </summary>
        Body,
        /// <summary>
        /// 请求或响应头
        /// </summary>
        Header,
        /// <summary>
        /// 查询
        /// </summary>
        Query,
        /// <summary>
        /// 路由
        /// </summary>
        Route,
    }
}
