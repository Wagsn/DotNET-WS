using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 歌曲收藏请求体
    /// </summary>
    public class SongCollectionRequest : RequestBase
    {
        public string SongId { get; set; }
    }
}
