using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Dto
{
    /// <summary>
    /// 歌曲推荐请求体，只需要传入用户ID，然后通过(推荐算法：喜欢的歌手、曲风（Tag）、收藏歌单中搜索一些)得出返回的歌曲列表，
    /// </summary>
    public class SongRecommendRequest : RequestBase {}
}
