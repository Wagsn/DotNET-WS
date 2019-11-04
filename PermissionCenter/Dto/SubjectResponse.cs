using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionCenter.Dto
{
    public class SubjectResponse
    {
        /// <summary>
        /// 是否允许登陆(通常主体为用户则允许登陆)
        /// </summary>
        public bool? AllowLogin { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 邮箱(用于登陆名或邮箱登陆)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 邮箱已确认(验证码登陆)
        /// </summary>
        public bool? EmailConfirmed { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 密码(第三方账号注册时为空，如：电话、邮箱、微信登陆)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 电话号码已确认(验证码登陆)
        /// </summary>
        public bool? PhoneConfirmed { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 登陆用户名(数字、字母、下划线)(若三方登陆则允许修改)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string WXOpenId { get; set; }
    }
}
