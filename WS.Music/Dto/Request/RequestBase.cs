using System;
using System.Collections.Generic;
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
        /// 用户Id，如果为0则表示为游客（或者通过IP设备用户等生成临时Id）
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }
    }
}
