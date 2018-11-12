using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Models
{
    /// <summary>
    /// 信息的种类有站内广播，评论，私信等
    /// </summary>
    public class Message
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 消息正文
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 消息类型，如：Text，JSON（RichText复合式消息），Image，Video，Radio（/Voice），Link
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 发送人ID
        /// </summary>
        public long FromUserId { get; set; }

        /// <summary>
        /// 发送人名称
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 接收人ID
        /// </summary>
        public long ToUserId { get; set; }

        /// <summary>
        /// 接收人名称
        /// </summary>
        public string ToUserName { get; set; }
    }
}
