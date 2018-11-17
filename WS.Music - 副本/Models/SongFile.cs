using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Models
{
    /// <summary>
    /// 一首歌曲可能有不同文件格式与尺寸，这里是用来描述一个歌曲文件实体，注：暂时不用，会提升系统复杂度
    /// </summary>
    public class SongFile
    {
        /// <summary>
        /// 歌曲文件ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 歌曲ID
        /// </summary>
        public long SongId { get; set; }

        /// <summary>
        /// 歌曲文件的类型（mp3）
        /// </summary>
        [MaxLength(63)]
        public string Type { get; set; }

        /// <summary>
        /// 歌曲文件的大小/B
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 歌曲文件的URL
        /// </summary>
        [MaxLength(255)]
        public string Url { get; set; }
    }
}
