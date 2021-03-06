//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: SubjectPermissionRelation.cs
// * history : Created by T4 10/28/2019 16:27:46 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 主体权限关联表
	/// from "subject_permission_relation" table, "ws_unified_subject" database.
    /// </summary>
    [Table("subject_permission_relation")] 
    public class SubjectPermissionRelation
    {
        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        [Column("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
		[MaxLength(36)]
        [Column("PermissionId")]
        public string PermissionId { get; set; }

        /// <summary>
        /// 主体ID
        /// </summary>
		[MaxLength(36)]
        [Column("SubjectId")]
        public string SubjectId { get; set; }

    }
}
