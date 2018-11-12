using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Models;

namespace WS.Music.Models
{
    /// <summary>
    /// 歌单集合，用在精品歌单：全部|华语|欧美，暂时不用
    /// </summary>
    public class PlayListCollection : TraceUpdateBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
