//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: Permission.cs
// * history : Created by T4 10/28/2019 10:22:51 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 权限
	/// from "permission" table, "ws_traineeplan_test" database.
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 
        /// </summary>
		[MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[Key]
		[MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[MaxLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[MaxLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[Key]
		[MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[MaxLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[MaxLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[Key]
		[MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[MaxLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
		[MaxLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 唯一码
        /// </summary>
		[MaxLength(255)]
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
		[MaxLength(255)]
        public string Desc { get; set; }

        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
		[MaxLength(255)]
        public string Name { get; set; }

    }
}
