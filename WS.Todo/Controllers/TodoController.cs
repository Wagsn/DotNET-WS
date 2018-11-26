using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WS.Todo.Dto;
using WS.Todo.Models;
using WS.Todo.Stores;
using WS.Core.Text;
using WS.Core.Dto;
using System.ComponentModel.DataAnnotations;
using WS.Todo.Managers;
using Microsoft.EntityFrameworkCore;
using WS.Core.Log;

namespace WS.Todo.Controllers
{
    /// <summary>
    /// 待办控制器，做到用户登录，每个人的待办项是不一样的
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]  // api/todo
    [ApiController] 
    public class TodoController : ControllerBase
    {
        /// <summary>
        /// 待办管理
        /// </summary>
        protected ITodoItemManager<ITodoItemStore<ApplicationDbContext, TodoItem>> TodoItemManager { get; set; }

        protected ILogger Logger = LoggerManager.GetLogger("TodoController");

        /// <summary>
        /// 依赖注入，在/Startup.cs->ConfigureServices方法中，在/IServiceCollectionExtensions.cs#IServiceCollectionExtensions.AddUserDefined
        /// </summary>
        /// <param name="todoItemManager">待办项管理</param>
        public TodoController(ITodoItemManager<ITodoItemStore<ApplicationDbContext, TodoItem>> todoItemManager)
        {
            TodoItemManager = todoItemManager ?? throw new ArgumentNullException(nameof(todoItemManager));
        }

        /// <summary>
        /// 获取所有待办，如果要改响应体的话还需要改/wwwroot/js/site.js文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("all", Name ="GetsAllTodo")]  // api/todo
        public async Task<PagingResponseMessage<TodoItemJson>> GetAll([Required][FromBody]PageRequest request)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: " );
            Console.WriteLine(JsonHelper.ToJson(request));
            Logger.Trace("WS------ Request: \r\n{0}", JsonHelper.ToJson(request));
            PagingResponseMessage <TodoItemJson> response = new PagingResponseMessage<TodoItemJson>();
            // 模型验证在模型本身存在
            try
            {
                // 业务处理
                await TodoItemManager.List(response, request, default(CancellationToken));
            }
            catch (Exception e)
            {
                response.Wrap(ResponseDefine.ServiceError, e.Message);
                // 日志输出：服务器错误
                Console.WriteLine("WS------ ServiceError: \r\n" + e);
            }
            // 日志输出：响应体
            Console.WriteLine("WS------ Response: ");
            Console.WriteLine(JsonHelper.ToJson(response));
            return response;
        }

        /// <summary>
        /// Get Todo item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTodo")]  // ActionName
        public async Task<ResponseMessage<TodoItem>> GetById(long id)
        {
            Console.WriteLine("WS------ Request:  GetAll() ");  // 打印请求日志
            ResponseMessage<TodoItem> response = new ResponseMessage<TodoItem>();
            if (response.Extension == null)
            {
                response.Code = "404";
                response.Message = "Not Fund";
            }
            return response;
        }

        /// <summary>
        /// 新建待办
        /// </summary>
        /// <param name="request">待办创建请求体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseMessage<TodoItemJson>> Create([FromBody]ModelRequest<TodoItemJson> request)
        {
            Console.WriteLine("WS-- Request:  " + JsonHelper.ToJson(request));  // 打印请求日志
            ResponseMessage<TodoItemJson> response = new ResponseMessage<TodoItemJson>();

            // 模型验证在模型本身存在
            try
            {
                // 业务处理
                await TodoItemManager.CreateOrUpdate(response, request, default(CancellationToken));
            }
            catch (Exception e)
            {
                response.Wrap(ResponseDefine.ServiceError, e.Message);
                // 日志输出：服务器错误
                Console.WriteLine("WS------ ServiceError: \r\n" + e);
            }
            // 日志输出：响应体
            Console.WriteLine("WS------ Response: ");
            Console.WriteLine(JsonHelper.ToJson(response));
            return response;
        }

        /// <summary>
        /// 更新待办，根据传入的非空字段进行修改
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="item">编辑后的待办</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task Update([FromBody]ModelRequest<TodoItemJson> request)
        {
            Logger.Trace("WS------ Request: \r\n{0}", JsonHelper.ToJson(request));
            return;
        }

        /// <summary>
        /// 删除待办
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResponseMessage<TodoItemJson>> Delete([FromBody]ModelRequest<TodoItemJson> request)
        {
            Console.WriteLine("WS-- Request:  " + JsonHelper.ToJson(request));  // 打印请求日志

            ResponseMessage<TodoItemJson> response = new ResponseMessage<TodoItemJson>();

            // 模型验证在模型本身存在
            try
            {
                // 业务处理
                await TodoItemManager.Delete(response, request, default(CancellationToken));
            }
            catch (Exception e)
            {
                response.Wrap(ResponseDefine.ServiceError, e.Message);
                // 日志输出：服务器错误
                Console.WriteLine("WS------ ServiceError: \r\n" + e);
            }
            // 日志输出：响应体
            Console.WriteLine("WS------ Response: ");
            Console.WriteLine(JsonHelper.ToJson(response));
            return response;
        }
    }
}