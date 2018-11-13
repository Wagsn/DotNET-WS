using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Dto;
using WS.Core.Helpers;
using WS.Music.Dto;
using WS.Music.Managers;
using WS.Music.Models;
using WS.Music.Stores;

namespace WS.Music.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public ApplicationDbContext Context { get; }

        public RelPlayListSongStore _RelPlayListSongStore { get; }

        public RelUserPlayListStore _RelUserPlayListStore { get; set; }

        public SongStore _SongStore { get; } 

        public UserManager Manager { get; }

        /// <summary>
        /// 用户控制构造器
        /// </summary>
        /// <param name="applicationDbContext"></param>
        /// <param name="relUserPlayListStore"></param>
        /// <param name="relPlayListSongStore"></param>
        /// <param name="songStore"></param>
        /// <param name="userManager"></param>
        public UserController(
            ApplicationDbContext applicationDbContext, 
            RelUserPlayListStore relUserPlayListStore,
            RelPlayListSongStore relPlayListSongStore, 
            SongStore songStore,
            UserManager userManager
            )
        {
            Context = applicationDbContext;
            _RelPlayListSongStore = relPlayListSongStore;
            _RelUserPlayListStore = relUserPlayListStore;
            _SongStore = songStore;
            Manager = userManager;
        }

        /// <summary>
        /// 用户收藏歌曲
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("songcollection")]
        public async Task<ResponseMessage> SongCollection([FromBody]SongCollectionRequest request)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: \r\n" + JsonHelper.ToJson(request));
            // 创建响应体
            ResponseMessage response = new ResponseMessage();
            // 模型验证
            if (!Util.ModelValidCheck(ModelState, response))
            {
                return response;
            }
            try
            {
                /// 业务处理
                await Manager.CollectionSongAsync(response, request);
            }
            catch (Exception e)
            {
                response.Code = ResponseDefine.ServiceError;
                response.Message += "\r\n" + e.Message;
                // 日志输出：服务器错误
                Console.WriteLine("WS------ ServiceError: \r\n" + e);
            }
            // 日志输出：响应体
            Console.WriteLine("WS------ Response: \r\n" + response != null ? JsonHelper.ToJson(response) : "");
            return response;
        }

        /// <summary>
        /// 通过ID查询用户信息，TODO：建立请求分发中心，request携带请求码code来判断是哪个Manager来处理事务然后返回结果
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseMessage<UserJson>> Get([FromBody]UserJson user)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: \r\n" + user);
            // 创建响应体
            ResponseMessage<UserJson> response = new ResponseMessage<UserJson>();
            // 模型验证
            if (!Util.ModelValidCheck(ModelState, response))
            {
                return response;
            }
            try
            {
                /// 业务处理，TODO：<see cref="WSControllerBase.Get{Ext}(string)"/>
                response.Extension = await Manager.GetUserJson(response, user);
            }
            catch (Exception e)
            {
                response.Code = ResponseDefine.ServiceError;
                response.Message += "\r\n"+e.Message;
                // 日志输出：服务器错误
                Console.WriteLine("WS------ ServiceError: \r\n" + e);
            }
            if (response.Extension == null)
            {
                response.Code = ResponseDefine.NotFound;
                // 日志输出：找不到资源
                Console.WriteLine("WS------ NotFund: \r\n" + "");
            }
            // 日志输出：响应体
            Console.WriteLine("WS------ Response: \r\n" + response != null ? JsonHelper.ToJson(response) : "");
            return response;
        }

        /// <summary>
        /// Body，歌曲推荐，可以创建个DiscoverController将一些公共的对象放进去，如：发现音乐、发现歌手、发现榜单等等
        /// 根据用户ID在RelUserPlayList中查询Type为Recommend的PlayListId，再在RelPlayListSong中查询出List<SongId>，最后在Song中查出List<Song>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("recommend")]
        public async Task<ResponseMessage<List<Song>>> Recommend([FromBody] SongRecommendRequest request)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: \r\n" + request);
            // 创建响应体
            ResponseMessage<List<Song>> response = new ResponseMessage<List<Song>>();
            // 模型验证只会对存在的字段的格式进行验证和请求体的非空验证，所以我们需要进行字段的非空验证和有效性验证
            // 因为很多验证所以封装成方法
            if (UserIsNull(response, request.User)) return response;
            // 有效性检查：不为0且在数据库中存在
            if (request.User.Id == null)
            {
                response.Wrap(ResponseDefine.BadRequset, "你登陆用户的ID等于0？");
                return response;
            }

            User user = await Manager.GetUser(response, request.User);
            if (user == null) return response;

            try
            {
                // 根据用户ID在RelUserPlayList中查询Type为Recommend的PlayListId
                RelUserPlayList relUserPlayList = await _RelUserPlayListStore.ReadAsync(a => a.Where(b => b.UserId == user.Id && b.Type == "Recommend"), CancellationToken.None);  //await Context.RelUserPlayLists.Where(a => a.UserId == request.UserId && a._IsDeleted == false).AsNoTracking().SingleOrDefaultAsync();
                var playListId = relUserPlayList.PlayListId;
                // 再在RelPlayListSong中查询出List<long>
                var songIds = await _RelPlayListSongStore.ListAsync(a => a.Where(b => b.PlayListId == playListId).Select(c => c.SongId), CancellationToken.None);
                // 最后在Song中查出List<Song>
                response.Extension = await _SongStore.ListAsync(a => a.Where(b => songIds.Contains(b.Id)), CancellationToken.None);
            }
            catch (Exception e)
            {
                response.Code = ResponseDefine.ServiceError;
                response.Message += "\r\n" + e.Message;
                // 日志输出：服务器错误
                Console.WriteLine("WS------ ServiceError: \r\n" + e);
            }
            if (response.Code == "0" && response.Extension == null)
            {
                response.Code = ResponseDefine.NotFound;
                // 日志输出：找不到资源
                Console.WriteLine("WS------ NotFund: \r\n" + "");
            }
            // 日志输出：响应体
            Console.WriteLine("WS------ Response: \r\n" + response != null ? JsonHelper.ToJson(response) : "");
            return response;
        }
        
        /// <summary>
        /// 判断用户存在并修改响应体
        /// </summary>
        /// <typeparam name="TSrc"></typeparam>
        /// <param name="response"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        private bool UserIsNull<TSrc>(ResponseMessage response, TSrc src)
        {
            if (src == null)
            {
                response.Code = ResponseDefine.BadRequset;
                response.Message += "\r\n" + "服务器未接收到你的用户信息，你需要登陆之后才能访问，如已登录，请检查你的请求体";
                return true;
            }
            return false;
        }
    }
}
