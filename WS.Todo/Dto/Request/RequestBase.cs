using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Dto.Request
{
    /// <summary>
    /// 分页查询请求
    /// </summary>
    public class PageRequestBase
    {
        /// <summary>
        /// 分页索引，大于等于1，
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页数量，1至50
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// 索引溢出的处理方式，返回错误，返回第一页，返回空数组
        /// </summary>
        public int? FlowType { get; set; }
    }
}
