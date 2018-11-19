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

        public SongStore _SongStore { get; set; }

        public UserStore TheUsers { get; set; }

        public IMapper _Mapper { get; set; }

        public UserManager(RelUserPlayListStore RelUserPlayListStore, RelPlayListSongStore RelPlayListSongStore, SongStore SongStore, UserStore theUsers, IMapper Mapper)
        {
            _RelUserPlayListStore = RelUserPlayListStore;
            _RelPlayListSongStore = RelPlayListSongStore;
            _SongStore = SongStore;
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
            if (request.User == null|| request.User.Id == null)
            {
                response.Code = ResponseDefine.BadRequset;
                response.Message += "\r\n" + "请求体未包含系统用户信息，用户未登陆不能进行收藏歌曲操作";
                return;
            }
            // 判断用户是否存在
            //var user = await TheUsers.ReadAsync(a => a.Where(b => b.Id == request.User.Id), cancellationToken);
            var user = TheUsers.ById(request.User.Id).SingleOrDefault();
            
            if (user==null)
            {
                response.Code = ResponseDefine.NotFound;
                response.Message += "\r\n"+ Define.User.NotFoundMsg;
                return;
            }
            // 找到PlayListId
            var RecommendPlayListId = await _RelUserPlayListStore.ReadAsync(a => a.Where(b => b.UserId == request.User.Id && b.Type == PlayListType.Recommend).Select(c=>c.PlayListId), cancellationToken);
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
        /// 收藏歌曲取消
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task CollectionSongCancel([Required]ResponseMessage response, [Required]SongCollectionRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Check arguments
            if (request.User == null || request.User.Id == null)
            {
                Define.Response.Wrap(response, Define.Response.BadRequsetCode, "请求体未包含系统用户信息，用户未登陆不能进行收藏歌曲操作");
                return;
            }
            if (request.SongId == null)
            {
                Define.Response.Wrap(response, Define.Response.BadRequsetCode, "取消歌曲收藏，其ID不能为null");
                return;
            }
            // Check whether user exist
            var user = TheUsers.ById(request.User.Id).SingleOrDefault();
            if (user == null)
            {
                Define.Response.Wrap(response, Define.Response.NotFoundCode, Define.User.NotFoundMsg);
                return;
            }
            // 歌曲是否存在

            // 通过用户查询收藏歌单
            var playListId = TheUsers.FindPlayListIdByType(request.User.Id, Define.PlayList.Type.Collection).SingleOrDefault();
            if (playListId == null)
            {
                // 如果之前没有创建可以在这里创建
                Define.Response.Wrap(response, Define.Response.NotFoundCode, "收藏歌单未找到，可能的原因是创建账号时没有默认创建收藏歌单！");
            }
            // 通过收藏歌单查询歌曲 PlayListStore.FindSongByPlayListId
            var songId =_SongStore.FindIdByPlayListId(playListId).SingleOrDefault();
            // 比对歌曲
            if (songId == null)
            {
                response.Message += "\r\n" + "你所取消收藏的歌曲不在你的收藏歌单中！";
            }
            else  // 歌曲在歌单中则断开链接，软删除 RelPlayListSong
            {
                await _RelPlayListSongStore.Delete(request.User.Id, playListId, songId);
                response.Message += "\r\n"+"歌曲取消成功！";
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

        /// <summary>
        /// 获得用户通过用户Id
        /// </summary>
        /// <param name="response"></param>
        /// <param name="userJson"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
            // 放到Store中去
            var query = from u in TheUsers.Context.Users
                        where u.Id == userJson.Id || u.Name == userJson.Name || u.Email == userJson.Email  // those value is only one in db
                        select new User(u);
            return query;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Create([Required]ResponseMessage<UserJson> response, [Required]UserSignUpRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            // 检查参数是否异常
            var signupUserJson = request.SignupUser;  // 不采用request.User的原因是为了后台手动注册时也有账户登陆
            if (signupUserJson == null || signupUserJson.Name == null || signupUserJson.Pwd == null)
            {
                Define.Response.Wrap(response, ResponseDefine.BadRequset, "用户注册信息不全");
                return;
            }
            // 检查用户是否已经存在
            var dbuser = TheUsers.ByName(signupUserJson.Name).SingleOrDefault();
            if (dbuser != null)
            {
                Define.Response.Wrap(response, ResponseDefine.BadRequset, "该用户名已经存在，不能重复注册，若是你注册的请登陆或到密码找回处找回密码");
                return;
            }
            // 创建用户
            dbuser = _Mapper.Map<User>(signupUserJson);
            dbuser.Id = Guid.NewGuid().ToString();
            dbuser._CreateUserId = dbuser.Id;
            var user = await TheUsers.CreateAsync(dbuser, cancellationToken);
            if (user == null)
            {
                Define.Response.Wrap(response, ResponseDefine.ServiceError, "在数据库插入数据失败");
                return;
            }
            response.Extension = _Mapper.Map<UserJson>(user);
        }

        /// <summary>
        /// 用户信息更新
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public async Task Update([Required]ResponseMessage<UserJson> response, [Required]UserUpdateRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            // 检查参数格式
            if(request.User==null || request.User.Id == null || request.User.Name == null || request.User.Pwd == null)
            {
                Define.Response.Wrap(response, Define.Response.NotAllowCode, "你还没有登陆，不能够修改用户信息");
                return;
            }
            if(request.UpdateUser == null||request.UpdateUser.Id==null)
            {
                Define.Response.Wrap(response, Define.Response.BadRequsetCode, "修改用户信息的请求有误");
                return;
            }
            // 判断用户是否存在及其密码是否正确
            if (request.User.Id != request.UpdateUser.Id)
            {
                Define.Response.Wrap(response, Define.Response.NotSupportCode, "抱歉，暂时不支持修改其他账号的用户信息");  // 用户是不应该知道自己的ID的
            }
            // 检查参数有效
            var dbuser = TheUsers.ById(request.UpdateUser.Id).SingleOrDefault();
            if (dbuser == null)
            {
                Define.Response.Wrap(response, Define.Response.BadRequsetCode, Define.User.NotFoundMsg);
                return;
            }
            // 判断密码是否错误
            else if (dbuser.Pwd!=request.User.Pwd)
            {
                Define.Response.Wrap(response, Define.Response.BadRequsetCode, "用户密码错误");
                return;
            }
            // 用户数据更新: 将数据库中拿出的user更新几个变化的值
            dbuser._Update(_Mapper.Map<User>(request.UpdateUser));
            dbuser._UpdateUserId = request.User.Id;
            await TheUsers.UpdateAsync(dbuser, cancellationToken);
            response.Extension = _Mapper.Map<UserJson>(dbuser);  // 返回的User包含完整的用户信息
        }
    }
}
