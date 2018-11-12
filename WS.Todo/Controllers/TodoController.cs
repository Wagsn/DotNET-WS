using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Dto;
using TodoApi.Models;
using TodoApi.Stores;
using WS.Core.Helpers;

namespace TodoApi.Controllers
{
    /// <summary>
    /// 待办控制器
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
        public async Task<ResponseMessage<List<TodoItem>>> GetAll()
        {
            Console.WriteLine("WS------ Request:  GetAll() " );  // 打印请求日志
            ResponseMessage<List<TodoItem>> response = new ResponseMessage<List<TodoItem>>();
            response.Extension = await Store.ListAsync(a => a.Where(b => true), CancellationToken.None);
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
        public async Task<ResponseMessage<TodoItem>> GetById(long id)
        {
            Console.WriteLine("WS------ Request:  GetAll() ");  // 打印请求日志
            ResponseMessage<TodoItem> response = new ResponseMessage<TodoItem>();
            response.Extension = await Store.ReadAsync(a => a.Where(b => b.Id == id), CancellationToken.None);
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
        public async Task<ResponseMessage<TodoItem>> Create([FromBody]TodoItemCreateRequest request)
        {
            Console.WriteLine("WS-- Request:  "+JsonHelper.ToJson(request));  // 打印请求日志
            ResponseMessage<TodoItem> response = new ResponseMessage<TodoItem>();
            response.Extension = await Store.CreateAsync(new TodoItem
            {
                CreateUserId = request.UserId,
                Name = request.Name,
                IsComplete = request.IsComplete
            }, CancellationToken.None);
            if (response.Extension == null)
            {
                response.Code = ResponseCodeDefines.ServiceError;  // 添加失败
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
        public async Task Update(long userId, TodoItem item)
        {
            Console.WriteLine("WS-- Request:  id="+userId+", item=" + JsonHelper.ToJson(item));
            await Store.UpdateAsync(item, CancellationToken.None);
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
            await Store.DeleteIfAsync(a => a.Where(b => b.Id == id), CancellationToken.None);
            return;
        }
    }
}