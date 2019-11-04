//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: PermissionRelation.cs
// * history : Created by T4 10/28/2019 16:27:46 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 权限关联表
	/// from "permission_relation" table, "ws_unified_subject" database.
    /// </summary>
    [Table("permission_relation")] 
    public class PermissionRelation
    {
        /// <summary>
        /// 子ID
        /// </summary>
		[MaxLength(36)]
        [Column("ChildId")]
        public string ChildId { get; set; }

        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        [Column("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 直接关联
        /// </summary>
        [Column("IsDirect")]
        public bool IsDirect { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
		[MaxLength(36)]
        [Column("ParentId")]
        public string ParentId { get; set; }

    }
}
