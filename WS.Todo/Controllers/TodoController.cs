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
        /// 待办存储
        /// </summary>
        protected TodoItemStore Store { get; }

        /// <summary>
        /// 依赖注入，在/Startup.cs->ConfigureServices方法中
        /// </summary>
        /// <param name="store">待办项存储</param>
        public TodoController(TodoItemStore store)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
        }

        /// <summary>
        /// 获取所有待办，如果要改响应体的话还需要改/wwwroot/js/site.js文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]  // api/todo
        public async Task<ResponseMessage<List<Models.TodoItem>>> GetAll()
        {
            Console.WriteLine("WS------ Request:  GetAll() " );  // 打印请求日志
            ResponseMessage<List<Models.TodoItem>> response = new ResponseMessage<List<Models.TodoItem>>();
            if (response.Extension ==null)
            {
                response.Code = "404";
                response.Message = "Not Fund";
            }
            return response;
        }

        /// <summary>
        /// Get Todo item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTodo")]  // ActionName
        public async Task<ResponseMessage<Models.TodoItem>> GetById(long id)
        {
            Console.WriteLine("WS------ Request:  GetAll() ");  // 打印请求日志
            ResponseMessage<Models.TodoItem> response = new ResponseMessage<Models.TodoItem>();
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
        public ResponseMessage<Models.TodoItem> Create([FromBody]TodoItemRequest request)
        {
            Console.WriteLine("WS-- Request:  " + JsonHelper.ToJson(request));  // 打印请求日志
            ResponseMessage<Models.TodoItem> response = new ResponseMessage<Models.TodoItem>();
            if (response.Extension == null)
            {
                response.Code = ResponseDefine.ServiceError;  // 添加失败
                response.Message = "添加失败，可能传入的参数有误!";
                return response;
            }
            return response;
        }

        /// <summary>
        /// 更新待办，根据传入的非空字段进行修改
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="item">编辑后的待办</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task Update(long userId, Models.TodoItem item)
        {
            Console.WriteLine("WS-- Request:  id="+userId+", item=" + JsonHelper.ToJson(item));
            return;
        }

        /// <summary>
        /// 删除待办
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            return;
        }
    }
}