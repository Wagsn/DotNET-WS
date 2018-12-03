using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using WS.Core.Dto;
using WS.Log;
using WS.Text;

using WS.Todo.Dto;
using WS.Todo.Managers;
using WS.Todo.Models;
using WS.Todo.Stores;

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

        /// <summary>
        /// 
        /// </summary>
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
            Logger.Trace("[{0}Action] Request: \r\n{1}", "GetAll", JsonUtil.ToJson(request));
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
                Logger.Error("[{0}Action] ServiceError: \r\n{1}", "GetAll", e);
            }
            // 日志输出：响应体
            Logger.Trace("[{0}Action] Response: \r\n{1}", "GetAll", JsonUtil.ToJson(response));
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
            // 打印请求日志
            Logger.Trace("[{0}Action] Request: \r\n{1}", "Create", JsonUtil.ToJson(request));

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
                Logger.Error("[{0}Action] ServiceError: \r\n{1}", "Create", e);
            }
            // 日志输出：响应体
            Logger.Trace("[{0}Action] Response: \r\n{1}", "Create", JsonUtil.ToJson(response));
            return response;
        }

        /// <summary>
        /// 更新待办，根据传入的非空字段进行修改
        /// </summary>
        /// <param name="request">单一模型请求</param>
        /// <returns></returns>
        [HttpPost("edittodo", Name ="EditTodo")]
        public async Task<ResponseMessage<TodoItemJson>> Update([FromBody]ModelRequest<TodoItemJson> request)
        {
            Logger.Trace("[{0}] Request: \r\n{1}", "TodoUpdate", JsonUtil.ToJson(request));
            ResponseMessage<TodoItemJson> response = new ResponseMessage<TodoItemJson>();

            try
            {
                // 业务调用
                await TodoItemManager.CreateOrUpdate(response, request, default(CancellationToken));
            }
            catch (Exception e)
            {
                response.Wrap(ResponseDefine.ServiceError, e.Message);
                // 日志输出：服务器错误
                Logger.Error("ServiceError: \r\n{0}", e);
            }

            // 日志输出：响应体
            Logger.Trace("Response: \r\n{0}", JsonUtil.ToJson(response));
            return response;
        }

        /// <summary>
        /// 删除待办
        /// </summary>
        /// <param name="request">单一模型请求</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResponseMessage<TodoItemJson>> Delete([FromBody]ModelRequest<TodoItemJson> request)
        {
            // 打印请求日志
            Logger.Trace("Request: \r\n{0}", JsonUtil.ToJson(request));

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
                Logger.Error("ServiceError: \r\n{0}", e);
            }
            // 日志输出：响应体
            Logger.Trace("Response: \r\n{0}", JsonUtil.ToJson(response));
            return response;
        }
    }
}