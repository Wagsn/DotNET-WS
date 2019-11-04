//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: SubjectResource.cs
// * history : Created by T4 10/28/2019 16:27:46 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 主体资源表
	/// from "subject_resource" table, "ws_unified_subject" database.
    /// </summary>
    [Table("subject_resource")] 
    public class SubjectResource
    {
        /// <summary>
        /// 键(NickName, OfficialName-官方名称：用户-姓名、公司-公司名、部门-部门名)
        /// </summary>
		[Key]
		[MaxLength(255)]
        [Column("Key")]
        public string Key { get; set; }

        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        [Column("SubjectId")]
        public string SubjectId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
		[MaxLength(255)]
        [Column("Value")]
        public string Value { get; set; }

    }
}
