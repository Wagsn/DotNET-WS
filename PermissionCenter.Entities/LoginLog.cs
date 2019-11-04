//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: LoginLog.cs
// * history : Created by T4 10/28/2019 16:27:46 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 登陆日志
	/// from "login_log" table, "ws_unified_subject" database.
    /// </summary>
    [Table("login_log")] 
    public class LoginLog
    {
        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        [Column("Id")]
        public string Id { get; set; }

        /// <summary>
        /// 登陆IP
        /// </summary>
		[MaxLength(255)]
        [Column("LoginIp")]
        public string LoginIp { get; set; }

        /// <summary>
        /// 登陆时间
        /// </summary>
        [Column("LoginTime")]
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 主体ID
        /// </summary>
		[MaxLength(36)]
        [Column("SubjectId")]
        public string SubjectId { get; set; }

        /// <summary>
        /// 主体名称
        /// 组织名或用户名
        /// </summary>
		[MaxLength(255)]
        [Column("SubjectName")]
        public string SubjectName { get; set; }

    }
}
