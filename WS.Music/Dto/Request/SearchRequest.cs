using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 搜索请求
    /// </summary>
    public class SearchRequest : RequestBase
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
    }
}
