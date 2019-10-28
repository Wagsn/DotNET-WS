//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: PermissionRelation.cs
// * history : Created by T4 10/28/2019 10:22:51 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 权限关联表
	/// from "permission_relation" table, "ws_unified_subject" database.
    /// </summary>
    public class PermissionRelation
    {
        /// <summary>
        /// 子ID
        /// </summary>
		[MaxLength(36)]
        public string ChildId { get; set; }

        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 直接关联
        /// </summary>
        public bool IsDirect { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
		[MaxLength(36)]
        public string ParentId { get; set; }

    }
}
