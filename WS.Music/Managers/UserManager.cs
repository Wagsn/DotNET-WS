using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Dto;
using WS.Music.Common;
using WS.Music.Dto;
using WS.Music.Models;
using WS.Music.Stores;

namespace WS.Music.Managers
{
    /// <summary>
    /// 用户管理，业务层，根据用户查询其歌单等等操作
    /// </summary>
    public class UserManager
    {
        //public UserStore 
        public RelUserPlayListStore _RelUserPlayListStore { get; set; }

        public RelPlayListSongStore _RelPlayListSongStore { get; set; }

        public UserStore Store { get; set; }

        public UserManager
            (
            UserStore userStore,
            RelUserPlayListStore relUserPlayListStore, 
            RelPlayListSongStore relPlayListSongStore
            )
        {
            Store = userStore;
            _RelUserPlayListStore = relUserPlayListStore;
            _RelPlayListSongStore = relPlayListSongStore;
        }

        /// <summary>
        /// 用户收藏歌曲，通过UserId在UserPlayList中找到RecommendPlayList，在PlayListSong中添加记录，需要对SongId进行有效性验证
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="songId">歌曲ID</param>
        public async Task CollectionSongAsync([Required]ResponseMessage response, [Required]SongCollectionRequest request)
        {
            // 判断用户是否存在
            var userId = await Store.ReadAsync(a => a.Where(b => b.Id == request.UserId).Select(c=>c.Id), CancellationToken.None);
            if (userId == null)
            {
                response.Code = ResponseDefine.NotFound;
                response.Message += "\r\n"+ Define.User.NotFoundMsg;
            }
            // 找到PlayListId
            var RecommendPlayListId = await _RelUserPlayListStore.ReadAsync(a => a.Where(b => b.UserId == request.UserId && b.Type == PlayListType.Recommend).Select(c=>c.PlayListId), CancellationToken.None);
            // 找不到歌单
            if(RecommendPlayListId==null)
            {
                response.Code = ResponseDefine.NotFound;
                response.Message += "\r\n" + Define.PlayList.NotFoundMsg;
                return;
            }
            // 判断是否已经收藏
            var playListSong = _RelPlayListSongStore.ReadAsync(a => a.Where(b => b.SongId == request.SongId), CancellationToken.None);
            if (playListSong == null)
            {
                // 判断歌曲是否存在
                //var song = 
                // 在RelPlayListSong中插入数据
                await _RelPlayListSongStore.CreateAsync(new Models.RelPlayListSong
                {
                    PlayListId = RecommendPlayListId,
                    SongId = request.SongId
                }, CancellationToken.None);
            }
            else
            {
                response.Code = ResponseDefine.PostRepeat;
            }
        }

        /// <summary>
        /// 登陆用户存在检查，不存在修改响应体状态码和追加信息
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        public async Task<User> UserGetAsync([Required]ResponseMessage response, [Required]UserJson userJson)
        {
            User user = await Store.ReadAsync(a => a.Where(b => b.Id == userJson.Id), CancellationToken.None);
            // 判断用户是否存在
            if (user == null)
            {
                Define.Response.UserNotFound(response, userJson);
            }
            return user;
        }
    }
}
