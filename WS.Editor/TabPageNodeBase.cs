using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS.Editor
{
    /// <summary>
    /// 用来保存分页信息
    /// </summary>
    public class TabPageNodeBase : ITabPageNode
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 分页类型（Web、Edit、View、Read）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        ///// <summary>
        ///// 分页
        ///// </summary>
        //public TabPage Page { get; set; }
    }

}
