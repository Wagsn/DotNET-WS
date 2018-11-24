using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Models
{
    /// <summary>
    /// 数据库更新追踪，实现软删除，以及存储数据库更新信息（这里是只保存当前副本，建议采用变更表，这样对当前表的压力较小）
    /// </summary>
    public abstract class TraceUpdate
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string _CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? _CreateTime { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public string _UpdateUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? _UpdateTime { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        public string _DeleteUserId { get; set; }

        /// <summary>
        /// 删除人时间
        /// </summary>
        public DateTime? _DeleteTime { get; set; }

        /// <summary>
        /// 删除状态，软删除（在数据库存在，在客户端不存在）
        /// </summary>
        public bool _IsDeleted { get; set; }
    }
}