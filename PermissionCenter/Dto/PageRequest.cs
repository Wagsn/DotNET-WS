using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionCenter.Dto
{
    /// <summary>
    /// 分页请求体
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 分页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
    }
}
