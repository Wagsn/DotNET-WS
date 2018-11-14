using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 用户信息更新请求
    /// </summary>
    public class UserUpdateRequest : RequestBase
    {
        /// <summary>
        /// 要更新的用户
        /// </summary>
        public UserJson UpdateUser { get; set; }
    }
}
