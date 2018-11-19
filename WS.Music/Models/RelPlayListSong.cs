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
    /// 歌单与歌曲的关联实体，（多对多）,关联表不需要那么多其它数据
    /// </summary>
    public class RelPlayListSong
    {
        /// <summary>
        /// 歌单ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string PlayListId { get; set; }

        /// <summary>
        /// 歌曲ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string SongId { get; set; }

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

        public RelPlayListSong()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rps"></param>
        public RelPlayListSong(RelPlayListSong rps)
        {
            PlayListId = rps.PlayListId;
            SongId = rps.SongId;
            PlayList = rps.PlayList;
            Song = rps.Song;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="rps"></param>
        public virtual void _Update(RelPlayListSong rps)
        {
            PlayListId = rps.PlayListId??PlayListId;
            SongId = rps.SongId??SongId;
            PlayList = rps.PlayList??PlayList;
            Song = rps.Song??Song;
        }
    }
}
