﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Models
{
    /// <summary>
    /// 实体类基类，提供相似度判断函数，有必要子类重写
    /// </summary>
    public abstract class TraceUpdateBase : ITraceUpdate
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        [MaxLength(36)]
        public string _CreateUserId { get ; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? _CreateTime { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        [MaxLength(36)]
        public string _UpdateUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? _UpdateTime { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        [MaxLength(36)]
        public string _DeleteUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? _DeleteTime { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool _IsDeleted { get; set; }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="update"></param>
        public virtual void _Update(ITraceUpdate update)
        {
            _CreateUserId = update._CreateUserId??_CreateUserId;
            _CreateTime = update._CreateTime??_CreateTime;
            _UpdateUserId = update._UpdateUserId??_UpdateUserId;
            _UpdateTime = update._UpdateTime?? _UpdateTime;
            _DeleteUserId = update._DeleteUserId?? _DeleteUserId;
            _DeleteTime = update._DeleteTime?? _DeleteTime;
            _IsDeleted = update._IsDeleted;
        }

        /// <summary>
        /// 匹配相似度
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual bool _Equals(ITraceUpdate update)
        {
            return _CreateTime == update._CreateTime;
        }
    }
}
