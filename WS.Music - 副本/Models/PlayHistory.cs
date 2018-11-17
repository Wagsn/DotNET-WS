using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Models;

namespace WS.Music.Models
{
    /// <summary>
    /// 播放历史，如何生成：当客户端播放时，向服务器发送开始播放的消息，当切歌时发送播放结束的消息（如何关联两者，开始播放将会返回历史ID，通过UpdateTime来判断是否已经提交过）
    /// </summary>
    public class PlayHistory : TraceUpdateBase
    {
        /// <summary>
        /// 历史ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 歌曲ID，可能重复播放歌曲
        /// </summary>
        public long SongId { get; set; }

        /// <summary>
        /// 播放时间，开始播放时间
        /// </summary>
        public DateTime? PlayStartTime { get; set; }

        /// <summary>
        /// 播放结束时间
        /// </summary>
        public DateTime? PlayEndTime { get; set; }

        /// <summary>
        /// 播放时长
        /// </summary>
        public DateTime? PlayDuration { get; set; }

    }
}
