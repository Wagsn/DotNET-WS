//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: OperateLog.cs
// * history : Created by T4 10/28/2019 10:22:51 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 操作日志
	/// from "operate_log" table, "ws_unified_subject" database.
    /// </summary>
    public class OperateLog
    {
        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string OperDesc { get; set; }

        /// <summary>
        /// 操作对象ID
        /// </summary>
		[MaxLength(36)]
        public string OperId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperTime { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public int? OperType { get; set; }

        /// <summary>
        /// 主体ID
        /// </summary>
		[MaxLength(36)]
        public string SubjectId { get; set; }

    }
}
