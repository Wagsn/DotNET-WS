//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: Permission.cs
// * history : Created by T4 10/28/2019 16:27:46 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 权限
	/// from "permission" table, "ws_unified_subject" database.
    /// </summary>
    [Table("permission")] 
    public class Permission
    {
        /// <summary>
        /// 唯一码
        /// </summary>
		[MaxLength(255)]
        [Column("Code")]
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
		[MaxLength(255)]
        [Column("Desc")]
        public string Desc { get; set; }

        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        [Column("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
		[MaxLength(255)]
        [Column("Name")]
        public string Name { get; set; }

    }
}
