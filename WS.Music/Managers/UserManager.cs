using AutoMapper;
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

        public UserStore TheUsers { get; set; }

        public IMapper _Mapper { get; set; }

        public UserManager(RelUserPlayListStore RelUserPlayListStore, RelPlayListSongStore RelPlayListSongStore, UserStore theUsers, IMapper Mapper)
        {
            _RelUserPlayListStore = RelUserPlayListStore;
            _RelPlayListSongStore = RelPlayListSongStore;
            TheUsers = theUsers;
            _Mapper = Mapper;
        }



        /// <summary>
        /// 用户收藏歌曲，通过UserId在UserPlayList中找到RecommendPlayList，在PlayListSong中添加记录，需要对SongId进行有效性验证
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="songId">歌曲ID</param>
        public async Task CollectionSongAsync([Required]ResponseMessage response, [Required]SongCollectionRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            // 判断用户是否存在
            var userId = await TheUsers.ReadAsync(a => a.Where(b => b.Id == request.UserId).Select(c=>c.Id), cancellationToken);
            if (userId == null)
            {
                response.Code = ResponseDefine.NotFound;
                response.Message += "\r\n"+ Define.User.NotFoundMsg;
            }
            // 找到PlayListId
            var RecommendPlayListId = await _RelUserPlayListStore.ReadAsync(a => a.Where(b => b.UserId == request.UserId && b.Type == PlayListType.Recommend).Select(c=>c.PlayListId), cancellationToken);
            // 找不到歌单
            if(RecommendPlayListId==null)
            {
                response.Code = ResponseDefine.NotFound;
                response.Message += "\r\n" + Define.PlayList.NotFoundMsg;
                return;
            }
            // 判断是否已经收藏
            var playListSong = _RelPlayListSongStore.ReadAsync(a => a.Where(b => b.SongId == request.SongId), cancellationToken);
            if (playListSong == null)
            {
                // 判断歌曲是否存在
                //var song = 
                // 在RelPlayListSong中插入数据
                await _RelPlayListSongStore.CreateAsync(new RelPlayListSong
                {
                    PlayListId = RecommendPlayListId,
                    SongId = request.SongId
                }, cancellationToken);
            }
            else
            {
                response.Code = ResponseDefine.PostRepeat;
            }
        }

        /// <summary>
        /// 获取用户，满足条件的：不存在修改响应体状态码和追加信息，或|且
        /// </summary>
        /// <param name="response">响应体</param>
        /// <param name="request">请求体</param>
        public async Task<UserJson> GetUserJson([Required]ResponseMessage response, [Required]UserJson userJson, CancellationToken cancellationToken = default(CancellationToken))
        {
            UserJson user = _Mapper.Map<UserJson>(await TheUsers.ReadAsync(a => a.Where(b => b.Id == userJson.Id), cancellationToken));
            // 判断用户是否存在
            if (user == null)
            {
                Define.Response.UserNotFound(response, userJson);
            }
            return user;
        }

        public async Task<User> GetUser([Required]ResponseMessage response, [Required]UserJson userJson, CancellationToken cancellationToken = default(CancellationToken))
        {
            User user = await TheUsers.ReadAsync(a => a.Where(b => b.Id == userJson.Id), cancellationToken);
            // 判断用户是否存在
            if (user == null)
            {
                Define.Response.UserNotFound(response, userJson);
            }
            return user;
        }

        /// <summary>
        /// 通过或运算来查询User
        /// </summary>
        /// <param name="response"></param>
        /// <param name="userJson"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IQueryable<User> GetUsersOr([Required]ResponseMessage response, [Required]UserJson userJson, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = from u in TheUsers.Context.Users
                        where u.Id == userJson.Id || u.Name == userJson.Name || u.Email == userJson.Email  // those value is only one in db
                        select new User(u);
            return query;
        }
    }
}
