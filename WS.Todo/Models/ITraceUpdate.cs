using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    /// <summary>
    /// 数据库更新追踪，实现软删除，以及存储数据库更新信息
    /// </summary>
    public interface ITraceUpdate
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        long? CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        long? UpdateUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        long? DeleteUserId { get; set; }

        /// <summary>
        /// 删除人时间
        /// </summary>
        DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 删除状态，软删除（在数据库存在，在客户端不存在）
        /// </summary>
        bool? IsDeleted { get; set; }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="update"></param>
        void Update(ITraceUpdate update);
    }
}
