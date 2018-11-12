using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 发送，用于请求与响应
    /// </summary>
    public class SendJson
    {
        /// <summary>
        /// 发送ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 消息发送方式类型：群发Massive，私发private，组内group，
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 发送用户ID
        /// </summary>
        public long FromUserId { get; set; }

        /// <summary>
        /// 发送用户
        /// </summary>
        public UserJson FromUser { get; set; }

        /// <summary>
        /// 发送时间，在重复发送时，以这个为重复判断标志
        /// </summary>
        public DateTime? FromTime { get; set; }

        /// <summary>
        /// 接收用户ID
        /// </summary>
        public long ToUserId { get; set; }
        
        /// <summary>
        /// 接收用户，发送消息请求时不用给出用户的详细数据
        /// </summary>
        public UserJson ToUser { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime? ToTime { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public long MsgId { get; set; }

        /// <summary>
        /// 消息内容，或者
        /// </summary>
        public MessageJson Msg { get; set; }
    }
}
