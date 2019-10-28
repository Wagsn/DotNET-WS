//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: SubjectPermissionExpansion.cs
// * history : Created by T4 10/28/2019 10:22:51 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 主体权限扩展表
    /// 将主体自关联表和权限自关联表进行扩展，让每一个主体和对应的每一个权限关联
	/// from "subject_permission_expansion" table, "ws_unified_subject" database.
    /// </summary>
    public class SubjectPermissionExpansion
    {
        /// <summary>
        /// 权限ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        public string PermissionId { get; set; }

        /// <summary>
        /// 主体ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        public string SubjectId { get; set; }

    }
}
