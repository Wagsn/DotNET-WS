using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using WS.Core.Models;

namespace WS.Music.Models
{
    /// <summary>
    /// 歌曲与专辑的关联实体（多对多）
    /// </summary>
    public class RelSongAlbum : TraceUpdateBase
    {
        /// <summary>
        /// 歌曲ID
        /// </summary>
        [MaxLength(36)]
        public string SongId { get; set; }

        /// <summary>
        /// 专辑ID
        /// </summary>
        [MaxLength(36)]
        public string AlbumId { get; set; }

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
