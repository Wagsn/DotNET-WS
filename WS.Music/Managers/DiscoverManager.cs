using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Dto;
using WS.Music.Dto;
using WS.Music.Stores;

namespace WS.Music.Managers
{
    /// <summary>
    /// 发现管理
    /// </summary>
    public class DiscoverManager
    {
        public SongStore _SongStore { get; set; }
        public UserStore _UserStore { get; set; }

        public DiscoverManager(SongStore SongStore, UserStore UserStore)
        {
            _SongStore = SongStore;
            _UserStore = UserStore;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        public void Search(ResponseMessage<object> response, SearchRequest request)
        {
            
        }
    }
}
