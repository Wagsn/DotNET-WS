using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Models
{
    /// <summary>
    /// 音乐专辑实体
    /// </summary>
    public class Album
    {
        /// <summary>
        /// 专辑ID
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 专辑名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 发行时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }

        /// <summary>
        /// 歌手列表（一般只有一个，用于合辑）
        /// </summary>
        [NotMapped]
        public List<Artist> Artists { get; set; }
    }
}
