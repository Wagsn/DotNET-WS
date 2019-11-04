//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: OperateLog.cs
// * history : Created by T4 10/28/2019 16:27:46 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 操作日志
	/// from "operate_log" table, "ws_unified_subject" database.
    /// </summary>
    [Table("operate_log")] 
    public class OperateLog
    {
        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        [Column("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        [Column("OperDesc")]
        public string OperDesc { get; set; }

        /// <summary>
        /// 操作对象ID
        /// </summary>
		[MaxLength(36)]
        [Column("OperId")]
        public string OperId { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Column("OperTime")]
        public DateTime OperTime { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [Column("OperType")]
        public int? OperType { get; set; }

        /// <summary>
        /// 主体ID
        /// </summary>
		[MaxLength(36)]
        [Column("SubjectId")]
        public string SubjectId { get; set; }

    }
}
