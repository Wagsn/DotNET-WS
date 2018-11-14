using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Dto;
using WS.Core.Helpers;
using WS.Music.Dto;
using WS.Music.Managers;

namespace WS.Music.Controllers
{
    /// <summary>
    /// 发现控制器，发现音乐、排行榜、歌单、专辑、歌手
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DiscoverController
    {
        public SongManager Manager { get; set; }

        public DiscoverController(SongManager manager)
        {
            Manager = manager;
        }



        /// <summary>
        /// 搜索歌曲
        /// </summary>
        /// <param name="word"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public ResponseMessage<object> Search([FromBody]SearchRequest request)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: \r\n" + JsonHelper.ToJson(request));
            // 创建响应体
            ResponseMessage<object> response = new ResponseMessage<object>();
            try
            {
                 // 业务处理
                 //if (type)
                 //Manager.
            }
            catch (Exception e)
            {
                Define.Response.Wrap(response, ResponseDefine.ServiceError, e.Message);
                // 日志输出：服务器错误
                Console.WriteLine("WS------ ServiceError: \r\n" + e);
            }
            // 日志输出：响应体
            Console.WriteLine("WS------ Response: \r\n" + response != null ? JsonHelper.ToJson(response) : "");
            return response;
        }
    }
}
