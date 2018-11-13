using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Models
{
    /// <summary>
    /// 信息的种类有站内广播，评论，私信等
    /// </summary>
    public class Message : Core.Models.TraceUpdateBase
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string Id { get; set; }

        /// <summary>
        /// 消息类型（text/image/location/link）动态News，博客Blog
        /// </summary>
        [MaxLength(31)]
        public string Type { get; set; }

        /// <summary>
        /// 消息标题：非必要的字段
        /// </summary>
        [MaxLength(63)]
        public string Title { get; set; }

        /// <summary>
        /// 消息详情（专用字段，链接Link是URL，文本是Text，图片Image是URL，富文本RichText是JSON，特殊也是JSON）
        /// </summary>
        [MaxLength(511)]
        public string Detail { get; set; }
    }
}
