using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceDataFlow
{
    /// <summary>
    /// HTTP请求描述
    /// 包含路径、请求头、请求体等所有信息
    /// </summary>
    public  class HttpRequestInfo
    {
        /// <summary>
        /// 模板ID（一个节点）
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 实例ID（一次请求）
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// POST/PUT/DELETE/GET
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// URL模板: "http://127.0.0.1:5000/api/user?id={Id}"
        /// </summary>
        public string UrlTemplate { get; set; }

        /// <summary>
        /// URL: "http://127.0.0.1:5000/api/user?id=13241654"
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求头描述
        /// </summary>
        public List<AttributeInfo> Header { get; set; }

        /// <summary>
        /// 请求体描述
        /// </summary>
        public List<AttributeInfo> Body { get; set; }

        /// <summary>
        /// 数据依赖描述
        /// </summary>
        public List<FieldDependencyInfo> FieldDependencies { get; set; }
    }
}
