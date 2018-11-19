using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using WS.Core.Models;
using WS.Music.Models;

namespace Music.Models
{
    /// <summary>
    /// 歌曲与艺人的关联实体，（多对多）
    /// </summary>
    public class RelSongArtist : TraceUpdateBase
    {
        /// <summary>
        /// 歌曲ID
        /// </summary>
        [MaxLength(36)]
        public string SongId { get; set; }

        /// <summary>
        /// 艺人ID
        /// </summary>
        [MaxLength(36)]
        public string ArtistId { get; set; }

        /// <summary>
        /// 歌曲
        /// </summary>
        [NotMapped]
        public Song Song { get; set; }

        /// <summary>
        /// 艺人
        /// </summary>
        [NotMapped]
        public Artist Artist { get; set; }
    }
}
