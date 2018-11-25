using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Dto
{
    /// <summary>
    /// 分页查询请求
    /// </summary>
    public class PageRequest : UserRequest
    {
        /// <summary>
        /// 分页索引，大于等于0，
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页数量，1至50
        /// </summary>
        [MinLength(1)]
        [MaxLength(50, ErrorMessage ="分页查询每页数量不超过50")]
        public int PageSize { get; set; }

        /// <summary>
        /// 索引溢出的处理方式（默认第一页），0返回第一页，1返回错误，2返回空数组
        /// </summary>
        public int FlowType { get; set; }
    }
}
