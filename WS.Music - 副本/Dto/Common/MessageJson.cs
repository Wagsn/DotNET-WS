using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 消息，将发送与消息分离是为了转发方便
    /// </summary>
    public class MessageJson
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 消息类型（text/image/location/link）动态News，博客Blog
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 消息标题：非必要的字段
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息详情（专用字段，链接Link是URL，文本是Text，图片Image是URL，富文本RichText是JSON，特殊也是JSON）
        /// </summary>
        public string Detail { get; set; }
    }

    /// <summary>
    /// 指定消息内容的消息
    /// </summary>
    /// <typeparam name="TEx"></typeparam>
    public class MessageJson<TEx> : MessageJson
    {

    }
}
