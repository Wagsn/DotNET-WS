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
    /// 歌单与歌曲的关联实体，（多对多）
    /// </summary>
    public class RelPlayListSong : TraceUpdateBase
    {
        /// <summary>
        /// 歌单ID
        /// </summary>
        [Key]
        public long PlayListId { get; set; }

        /// <summary>
        /// 歌曲ID
        /// </summary>
        [Key]
        public long SongId { get; set; }

        /// <summary>
        /// 歌单
        /// </summary>
        [NotMapped]
        public PlayList PlayList { get; set; }

        /// <summary>
        /// 歌曲
        /// </summary>
        [NotMapped]
        public Song Song { get; set; }
    }
}
