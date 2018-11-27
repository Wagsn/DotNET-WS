using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Dto;
using WS.Todo.Dto;
using WS.Todo.Models;
using WS.Todo.Stores;

namespace WS.Todo.Managers
{
    /// <summary>
    /// 待办管理器
    /// </summary>
    public class TodoItemManager : ITodoItemManager<ITodoItemStore<ApplicationDbContext, TodoItem>>
    {
        /// <summary>
        /// 存储器
        /// </summary>
        public ITodoItemStore<ApplicationDbContext, TodoItem> Store { get; set; }

        /// <summary>
        /// 用户核心信息存储
        /// </summary>
        public IUserBaseStore<ApplicationDbContext, UserBase> UserBaseStore { get; set; }

        /// <summary>
        /// 实体映射
        /// </summary>
        public IMapper Mapper { get; set; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="store"></param>
        /// <param name="userBaseStore"></param>
        /// <param name="mapper"></param>
        public TodoItemManager([Required]ITodoItemStore<ApplicationDbContext, TodoItem> store, [Required]IUserBaseStore<ApplicationDbContext, UserBase> userBaseStore, [Required]IMapper mapper )
        {
            Store = store;
            UserBaseStore = userBaseStore;
            Mapper = mapper;
        }



        /// <summary>
        /// 新建或更新，当第一次创建待办时，传入name和pwd，之后传入id和pwd
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        public async Task CreateOrUpdate([Required] ResponseMessage<TodoItemJson> response, [FromBody] ModelRequest<TodoItemJson> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            UserBase user = await UserChecck(response, request.User);
            if (user == null)
            {
                return;
            }
            if (request.model == null)
            {
                response.Code = ResponseDefine.BadRequset;
                response.Message += "待办项为空，无法执行操作";
                return;
            }
            else
            {
                // 不存在ID，创建操作
                if (string.IsNullOrWhiteSpace(request.model.Id))
                {
                    //  创建User与Todo的关联
                    TodoItem todo = await Store.Create(new TodoItem
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = request.model.Name,
                        Content = request.model.Content,
                        ExpectTime = request.model.ExpectTime,
                        _CreateUserId = user.Id
                    }, cancellationToken);
                    response.Extension = Mapper.Map<TodoItemJson>(todo);
                    response.Message += "新建待办项成功";
                    return;
                }
                // 待办项ID存在，更新操作
                else
                {
                    // Id 是否有效，是否存在User与Todo的关联以及todo是否存在
                    TodoItem todo = await Store.ById(user.Id, request.model.Id).SingleOrDefaultAsync(cancellationToken);
                    if (todo == null)
                    {
                        response.Wrap(ResponseDefine.NotFound, "找不到该待办项");
                        return;
                    }
                    else
                    {
                        todo.Name = request.model.Name;
                        todo.Content = request.model.Content;
                        todo.ExpectTime = request.model.ExpectTime;  // 期望时间，最后的时刻
                        todo.IsComplete = request.model.IsComplete;
                        if (request.model.IsComplete)
                        {
                            todo.ActualTime = DateTime.Now;
                        }
                        todo._UpdateUserId = user.Id;
                        todo._UpdateTime = user._UpdateTime;
                        response.Extension = Mapper.Map<TodoItemJson>(await Store.Update(todo, cancellationToken));
                        response.Message += "更新待办项成功";
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 删除待办项
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        public async Task Delete([Required] ResponseMessage<TodoItemJson> response, [FromBody] ModelRequest<TodoItemJson> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            UserBase user = await UserChecck(response, request.User);
            if (user == null)
            {
                return;
            }
            // 删除Todo
            await Store.DeleteIfId(user.Id, request.model.Id, cancellationToken);
            response.Extension = request.model;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task List([Required] PagingResponseMessage<TodoItemJson> response, [FromBody] PageRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            response.Extension = new List<TodoItemJson>();
            UserBase user = await UserChecck(response, request.User);
            if (user == null)
            {
                //response.Wrap(ResponseDefine.BadRequset, "用户名不存在");
                return;
            }
            var todos = await Store.List(user.Id, a => a).Select(s=>Mapper.Map<TodoItemJson>(s)).ToListAsync(cancellationToken);
            response.TotalCount = todos.Count;
            if (todos.Count == 0)
            {
                return;
            }
            // 分页查询，页数，TODO 写到LINQ中
            int lastPageSize = todos.Count % request.PageSize;
            int pageCount = todos.Count / request.PageSize + (lastPageSize > 0 ? 1 : 0);
            response.PageCount = pageCount;
            lastPageSize = lastPageSize > 0 ? lastPageSize : request.PageSize;
            // 索引超限，默认第一页
            if (pageCount < request.PageIndex+1)
            {
                // 获取第一页
                if (todos.Count > request.PageSize)
                {
                    response.Extension = todos.GetRange(0, request.PageSize);
                }
                else
                {
                    response.Extension = todos;
                }
                response.PageIndex = 0;
            }
            else
            {
                response.Extension = todos.GetRange(0, pageCount > request.PageIndex + 1?request.PageSize:lastPageSize);
                response.PageIndex = request.PageIndex;
            }
        }

        /// <summary>
        /// 用户检查
        /// </summary>
        /// <param name="response"></param>
        /// <param name="userJson"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<UserBase> UserChecck([Required]ResponseMessage response, UserJson userJson, CancellationToken cancellationToken = default(CancellationToken))
        {
            UserBase user = null;
            // 参数检查
            if (userJson == null)
            {
                response.Wrap(ResponseDefine.BadRequset, "不存在用户信息，未登录将不能执行该项操作");
                return null;
            }
            // 请求体：存在用户ID
            if (!string.IsNullOrWhiteSpace(userJson.Id))
            {
                user = await UserBaseStore.ById("System", userJson.Id).SingleOrDefaultAsync(cancellationToken);
                if (user == null)
                {
                    response.Wrap(ResponseDefine.BadRequset, "找不到该用户");
                }
                else
                {
                    if (user.Pwd != userJson.Pwd)
                    {
                        response.Wrap(ResponseDefine.BadRequset, "密码错误");
                    }
                }
            }
            // 请求体：存在用户Name
            else if (!string.IsNullOrWhiteSpace(userJson.Name))
            {
                user = await UserBaseStore.ByName("System", userJson.Name).SingleOrDefaultAsync(cancellationToken);
                // 该用户不存在
                if (user == null)
                {
                    // 创建用户
                    var uid = Guid.NewGuid().ToString();
                    user = await UserBaseStore.Create(new UserBase
                    {
                        Id = uid,
                        Name = userJson.Name,
                        Pwd = userJson.Pwd,
                        _CreateUserId = uid
                    }, cancellationToken);
                }
                // 该用户存在
                else
                {
                    // 密码错误
                    if (user.Pwd != userJson.Pwd)
                    {
                        response.Wrap(ResponseDefine.BadRequset, "密码错误");  // IncorrectPassWord
                    }
                }
            }
            else
            {
                response.Wrap(ResponseDefine.BadRequset, "用户ID和用户名不能同时为空");
            }
            return user;
        }
    }
}
