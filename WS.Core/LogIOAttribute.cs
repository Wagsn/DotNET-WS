using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// 打印请求体和响应体
    /// </summary>
    public class LogIOAttribute: TypeFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public LogIOAttribute() : base(typeof(LogIOImpl))
        {
        }


        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        private class LogIOImpl : IAsyncActionFilter
        {
            public LogIOImpl(ILogger<LogIOAttribute> logger)
            {
                Logger = logger;
            }

            private ILogger<LogIOAttribute> Logger { get; }


            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // Log Request
                var _data = new SortedDictionary<string, object>();
                HttpRequest request = context.HttpContext.Request;
                _data.Add("request.url", request.Path.ToString());
                _data.Add("request.headers", request.Headers.ToDictionary(x => x.Key, v => string.Join(";", v.Value.ToList())));
                _data.Add("request.method", request.Method);
                _data.Add("request.executeStartTime", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                // 获取请求body内容
                string reqest = await new StreamReader(context.HttpContext.Request.Body).ReadToEndAsync();
                if (request.Method.ToLower().Equals("post"))
                {
                    // 启用倒带功能，就可以让 Request.Body 可以再次读取
                    request.EnableRewind();

                    Stream stream = request.Body;
                    byte[] buffer = new byte[request.ContentLength.Value];
                    stream.Read(buffer, 0, buffer.Length);
                    _data.Add("request.body", Encoding.UTF8.GetString(buffer));

                    request.Body.Position = 0;
                }
                else if (request.Method.ToLower().Equals("get"))
                {
                    _data.Add("request.body", request.QueryString.Value);
                }
                // Log Response
                // 获取Response.Body内容
                var originalBodyStream = context.HttpContext.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.HttpContext.Response.Body = responseBody;

                    await next();

                    context.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                    _data.Add("response.body", await GetResponse(context.HttpContext.Response));
                    context.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                    _data.Add("response.executeEndTime", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                    await responseBody.CopyToAsync(originalBodyStream);
                }

                //_data.Add("response.body", response);
                //_data.Add("response.executeEndTime", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                var loggerName = context.Controller.GetType().Name;
                var actionName = context.ActionDescriptor.DisplayName.Split("(")[0].Split(".").Last().Trim();
                var route = string.Join(",", context.HttpContext.Request.Path);
                Logger.LogWarning($"loggerName:{loggerName}, actionName:{actionName}, route:{route}");
            }

            /// <summary>
            /// 获取响应内容
            /// </summary>
            /// <param name="response"></param>
            /// <returns></returns>
            public async Task<string> GetResponse(HttpResponse response)
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
                return text;
            }
        }
    }
}
