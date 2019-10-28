//-----------------------------------------------------------------------
// * version : 2.0
// * author  : T4 自动生成
// * FileName: Subject.cs
// * history : Created by T4 10/28/2019 10:22:51 
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;

namespace PermissionCenter.Entities
{
    /// <summary>
    /// 主体
	/// from "subject" table, "ws_unified_subject" database.
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// 是否允许登陆(通常主体为用户则允许登陆)
        /// </summary>
        public bool? AllowLogin { get; set; }

        /// <summary>
        /// 邮箱(用于登陆名或邮箱登陆)
        /// </summary>
		[MaxLength(511)]
        public string Email { get; set; }

        /// <summary>
        /// 邮箱已确认(验证码登陆)
        /// </summary>
        public bool? EmailConfirmed { get; set; }

        /// <summary>
        /// ID
        /// </summary>
		[Key]
		[MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 密码(第三方账号注册时为空，如：电话、邮箱、微信登陆)
        /// </summary>
		[MaxLength(511)]
        public string Password { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
		[MaxLength(31)]
        public string Phone { get; set; }

        /// <summary>
        /// 电话号码已确认(验证码登陆)
        /// </summary>
        public bool? PhoneConfirmed { get; set; }

        /// <summary>
        /// 登陆用户名(数字、字母、下划线)(若三方登陆则允许修改)
        /// </summary>
		[MaxLength(255)]
        public string UserName { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
		[MaxLength(127)]
        public string WXOpenId { get; set; }

    }
}
