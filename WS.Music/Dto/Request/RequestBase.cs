using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 请求体基类，包含公共请求体部分，如：用户验证体
    /// </summary>
    public class RequestBase
    {
        /// <summary>
        /// 登陆用户，将其从根数据中抽离出来
        /// </summary>
        public UserJson User { get; set; }
    }
}
