using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 发送消息的请求
    /// </summary>
    public class SendMsgRequest: RequestBase
    {
        /// <summary>
        /// 发送实体，包含发送人接收人ID和消息类型内容
        /// </summary>
        public SendJson Send { get; set; }
    }
}
