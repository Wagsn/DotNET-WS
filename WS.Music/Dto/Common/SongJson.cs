using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 歌曲信息
    /// </summary>
    public class SongJson
    {
        /// <summary>
        /// 歌曲ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string Id { get; set; }

        /// <summary>
        /// 歌曲名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        [MaxLength(511)]
        public string Description { get; set; }

        /// <summary>
        /// 歌曲的持续时长，可能不需要，因为通过歌曲文件可以得到
        /// </summary>
        public long? Duration { get; set; }

        /// <summary>
        /// 发行时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }

        /// <summary>
        /// 歌曲文件的URL，标准输出，如果想听其他规格的歌曲文件，请在SongFile中查找
        /// </summary>
        [MaxLength(255)]
        public string Url { get; set; }

        // User Album TagList（标签集）
    }
}
