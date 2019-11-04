using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionCenter.Dto
{
    public class SearchRequest : PageRequest
    {
        /// <summary>
        /// 关键词类型
        /// </summary>
        public int KeyType { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword { get; set; }
    }
}
