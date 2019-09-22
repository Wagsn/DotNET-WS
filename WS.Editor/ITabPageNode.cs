using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS.Editor
{
    /// <summary>
    /// 分页数据节点
    /// </summary>
    public interface ITabPageNode
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// 分页类型（Web、Edit、View、Read）
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        string Url { get; set; }

        ///// <summary>
        ///// 分页
        ///// </summary>
        //TabPage Page { get; set; }
    }
    public enum PageNodeType
    {
        View,
        Edit,
        Web,
        Read
    }
}
