using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WS.Core.Dto;
using WS.Core.Text;
using WS.Music.Dto;
using WS.Music.Managers;
using WS.Music.Models;
using WS.Music.Stores;

namespace WS.Music.Controllers
{


    /// <summary>
    /// 歌曲控制器，获取歌曲数据
    /// </summary>
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SongController : ControllerBase
    {
        /// <summary>
        /// 应用数据库上下文
        /// </summary>
        private ApplicationDbContext Context { get; }

        private SongManager _SongManager { get; set; }

        private SongStore _SongStore { get; set; }

        /// <summary>
        /// 歌曲控制器构造器
        /// </summary>
        /// <param name="applicationDbContext"></param>
        public SongController(ApplicationDbContext applicationDbContext, SongStore songStore, SongManager songManager)
        {
            Context = applicationDbContext;
            _SongManager = songManager;
            _SongStore = songStore;
        }

        /// <summary>
        /// 歌曲信息的录入|更新
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("entry")]
        public async Task<ResponseMessage>Entry([FromBody]SongInfoEntryRequest request)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: \r\n" + request);
            // 创建响应体
            ResponseMessage response = new ResponseMessage();
            // 模型验证
            if (!Util.ModelValidCheck(ModelState, response))
            {
                return response;
            }
            try
            {
                /// 业务处理，TODO：<see cref=""/>
                await _SongManager.SongEntryAsync(response, request);
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
        /// 歌曲模糊搜索
        /// 歌曲名称、专辑名称、歌手名称搜索OK，TODO 歌词、标签搜索
        /// </summary>
        /// <param name="request">歌曲搜索请求</param>
        /// <returns></returns>
        [HttpGet("search")]
        public ResponseMessage<List<SongJson>> Search([FromBody]SongSearchRequest request)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: \r\n" + request);
            // 创建响应体
            ResponseMessage<List<SongJson>> response = new ResponseMessage<List<SongJson>>();
            try
            {
                // 业务处理
                _SongManager.Serch(response, request);
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
        /// Query，通过Id查询Misic，返回JSON数据携带额外修饰数据，标准模板
        /// </summary>
        /// <param name="Id">音乐Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseMessage<Song>> Get([FromQuery]string Id)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: \r\n" + "Id: "+ Id);
            // 创建响应体
            ResponseMessage<Song> response = new ResponseMessage<Song>();
            // 模型验证
            if (!Util.ModelValidCheck(ModelState, response))
            {
                return response;
            }
            try
            {
                /// 业务处理，TODO：<see cref="WSControllerBase.Get{Ext}(long)"/>
                response.Extension = await _SongStore.ReadAsync(new Song { Id = Id }, CancellationToken.None);  // await Context.Songs.Where(a => a.Id == Id).AsNoTracking().SingleOrDefaultAsync();
            }
            catch(Exception e)
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
            Console.WriteLine("WS------ Response: \r\n" + response != null ?JsonHelper.ToJson(response) :"");
            return response;
        }

        /// <summary>
        /// 歌曲上传
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ResponseMessage> Post([FromBody] )
        //{
        //    ResponseMessage response = new ResponseMessage();
        //    return response;
        //}
    }
}
