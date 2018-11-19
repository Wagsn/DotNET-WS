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
        /// 关键字
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// 类型,(Song: default, Artist, Ablum, Lyric歌词: ╳, All)
        /// </summary>
        public string Type { get; set; }
    }
}
