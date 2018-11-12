using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    /// <summary>
    /// 实体类基类
    /// </summary>
    public abstract class TraceUpdateBase : ITraceUpdate
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        public long? CreateUserId { get ; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public long? UpdateUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        public long? DeleteUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="update"></param>
        public virtual void Update(ITraceUpdate update)
        {
            CreateUserId = update.CreateUserId??CreateUserId;
            CreateTime = update.CreateTime??CreateTime;
            UpdateUserId = update.UpdateUserId??UpdateUserId;
            UpdateTime = update.UpdateTime?? UpdateTime;
            DeleteUserId = update.DeleteUserId?? DeleteUserId;
            DeleteTime = update.DeleteTime?? DeleteTime;
            IsDeleted = update.IsDeleted?? IsDeleted;
        }
    }
}
