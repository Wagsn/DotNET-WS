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
    /// 消息控制器：聊天、动态、公告
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MsgController: ControllerBase
    {
        public UserManager _UserManager { get; }

        public SendManager _SendManager { get; }

        /// <summary>
        /// 发送消息请求
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("send")]
        public ResponseMessage SendMsg([FromBody]SendMsgRequest request)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: \r\n" + JsonHelper.ToJson(request));
            // 创建响应体
            ResponseMessage response = new ResponseMessage();
            // 参数检查：空检查与有效性检查

            try
            {
                /// 业务处理，TODO：<see cref="UserManager.CollectionSongAsync(ResponseMessage, SongCollectionRequest)"/>
                ///await _UserManager.CollectionSongAsync(response, request);
                response.Code = ResponseDefine.NotSupport;
            }
            catch (Exception e)
            {
                response.Code = ResponseDefine.ServiceError;
                response.Message += "\r\n" + e.Message;
                // 日志输出：服务器错误
                Console.WriteLine("WS------ ServiceError: \r\n" + e);
            }
            // 日志输出：响应体
            Console.WriteLine("WS------ Response: \r\n" + response != null ? JsonHelper.ToJson(response) : "");
            return response;
        }
    }
}
