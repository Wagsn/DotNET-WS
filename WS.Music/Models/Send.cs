using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Models;
using WS.Music.Dto;

namespace WS.Music.Models
{
    /// <summary>
    /// 发送实体
    /// </summary>
    public class Send : Core.Models.TraceUpdateBase
    {
        /// <summary>
        /// 发送ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 消息发送方式类型：群发Massive，私发private，组内group，
        /// </summary>
        [MaxLength(31)]
        public string Type { get; set; }

        /// <summary>
        /// 发送用户ID
        /// </summary>
        public long FromUserId { get; set; }

        /// <summary>
        /// 发送用户
        /// </summary>
        [NotMapped]
        public User FromUser { get; set; }

        /// <summary>
        /// 发送时间，不应该为null
        /// </summary>
        public DateTime? FromTime { get; set; }

        /// <summary>
        /// 接收用户ID
        /// </summary>
        public long ToUserId { get; set; }

        /// <summary>
        /// 接收用户，发送消息请求时不用给出用户的详细数据
        /// </summary>
        [NotMapped]
        public User ToUser { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime? ToTime { get; set; }

        /// <summary>
        /// 额外携带的内容
        /// </summary>
        [MaxLength(63)]
        public string Ext { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public long MsgId { get; set; }

        /// <summary>
        /// 消息内容，或者
        /// </summary>
        [NotMapped]
        public Message Msg { get; set; }

        public Send()
        {
        }

        public Send(SendJson send)
        {
            _Update(send);
        }

        public void _Update(SendJson send)
        {
            Id = send.Id;
            Type = send.Type ?? Type;
            FromUserId = send.FromUserId;
            ToUserId = send.ToUserId;
            MsgId = send.MsgId;
            FromTime = send.FromTime ?? FromTime;
            ToTime = send.ToTime ?? ToTime;
        }

        /// <summary>
        /// 数据更新
        /// </summary>
        /// <param name="update"></param>
        public override void _Update(ITraceUpdate update)
        {
            // 如果update不是Send类型则send为null
            var send = update as Send;

            Id = send.Id;
            Type = send.Type ?? Type;
            FromUserId = send.FromUserId;
            ToUserId = send.ToUserId;
            MsgId = send.MsgId;
            FromTime = send.FromTime ?? FromTime;
            ToTime = send.ToTime ?? ToTime;

            Ext = send.Ext ?? Ext;
        }

    }
}
