using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WS.Music.Models;

namespace WS.Music.Dto
{
    /// <summary>
    /// 歌曲信息录入请求，如果Id不存在则是录入新的信息，如果存在则是更新信息，歌手ID和专辑ID等可以为空，如果歌手名与专辑名不存在则会创建新的歌手或专辑，存在则创建关联
    /// 歌曲上传与歌曲信息上传的分离
    /// </summary>
    public class SongInfoEntryRequest : RequestBase
    {
        public SongInfo SongInfo { get; set; }
    }

    /// <summary>
    /// 歌曲信息，标准的请求响应元数据，用于与数据库实体隔离
    /// </summary>
    public class SongInfo
    {
        /// <summary>
        /// 歌曲ID，没有就是新增，有就是创建
        /// </summary>
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 歌曲名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 歌曲描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public long? Duration { get; set; }

        /// <summary>
        /// 发行时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }

        /// <summary>
        /// 艺人列表，修改为ArtistInfo
        /// </summary>
        public List<Artist> Artists { get; set; }

        /// <summary>
        /// 所在唱片|专辑，修改为AlbumInfo
        /// </summary>
        public Album Album { get; set; }

        // 发行公司
    }
}
