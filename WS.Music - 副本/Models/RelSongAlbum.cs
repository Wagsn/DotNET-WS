using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Models
{
    /// <summary>
    /// 歌曲与专辑的关联实体（多对多）
    /// </summary>
    public class RelSongAlbum
    {
        /// <summary>
        /// 歌曲ID
        /// </summary>
        [MaxLength(63)]
        public string SongId { get; set; }

        /// <summary>
        /// 专辑ID
        /// </summary>
        public long AlbumId { get; set; }

        /// <summary>
        /// 歌曲
        /// </summary>
        [NotMapped]
        public Song Song { get; set; }

        /// <summary>
        /// 专辑
        /// </summary>
        [NotMapped]
        public Album Album { get; set; }
    }
}
