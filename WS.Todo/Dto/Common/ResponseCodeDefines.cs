using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Dto
{
    /// <summary>
    /// 响应体返回码
    /// </summary>
    public class ResponseCodeDefines
    {
        // 成功
        public static readonly string SuccessCode = "0";

        // 参数错误
        public static readonly string ModelStateInvalid = "100";
        public static readonly string ArgumentNullError = "101";

        // 请求错误
        public static readonly string BadRequset = "400";
        public static readonly string NotFound = "404";
        public static readonly string NotAllow = "403";

        // 服务器错误
        public static readonly string ServiceError = "500";
    }
}
