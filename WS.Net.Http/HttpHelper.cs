using System;

namespace WS.Net.Http
{
    /// <summary>
    /// HTTP请求助手
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="TBody">响应体ResponseBody</typeparam>
        /// <param name="url">URL，可能带Params</param>
        /// <param name="method">HTTP方法</param>
        /// <param name="data">携带的数据，可能解析成Params或RequestBody</param>
        /// <returns></returns>
        public static TBody Send<TBody>(string url, string method, object data)
        {
            return default(TBody);
        }
    }
}
