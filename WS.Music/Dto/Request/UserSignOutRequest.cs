﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 用户登出请求体
    /// </summary>
    public class UserSignOutRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
    }
}
