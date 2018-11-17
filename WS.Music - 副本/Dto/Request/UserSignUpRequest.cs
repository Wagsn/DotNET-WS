using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 用户注册请求体
    /// </summary>
    public class UserSignUpRequest : RequestBase
    {
        /// <summary>
        /// 注册用户  不采用request.User的原因是为了后台手动注册时也有账户登陆
        /// </summary>
        public UserJson SignupUser { get; set; }
    }
}
