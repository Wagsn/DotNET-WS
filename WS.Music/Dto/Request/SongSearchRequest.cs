using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 歌曲搜索请求体
    /// </summary>
    public class SongSearchRequest : RequestBase
    {
        /// <summary>
        /// 歌曲名
        /// </summary>
        public string SongName { get; set; }

        /// <summary>
        /// 歌手名
        /// </summary>
        public string ArtistName { get; set; }
    }
}
