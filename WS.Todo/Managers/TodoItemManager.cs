using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WS.Core.Dto;
using WS.Todo.Dto;
using WS.Todo.Models;
using WS.Todo.Stores;

namespace WS.Todo.Managers
{
    /// <summary>
    /// 待办管理器
    /// </summary>
    public class TodoItemManager : ITodoItemManager<ITodoItemStore<DbContext, TodoItem>>
    {
        /// <summary>
        /// 存储器
        /// </summary>
        public ITodoItemStore<DbContext, TodoItem> Store { get; set; }

        /// <summary>
        /// 用户核心信息存储
        /// </summary>
        public IUserBaseStore<DbContext, UserBase> UserBaseStore { get; set; }

        /// <summary>
        /// 实体映射
        /// </summary>
        public IMapper Mapper { get; set; }


        public TodoItemManager([Required]ITodoItemStore<DbContext, TodoItem> store, [Required]IUserBaseStore<DbContext, UserBase> userBaseStore, [Required]IMapper mapper )
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
        public async Task CreateOrUpdate([Required] ResponseMessage<TodoItemJson> response, [FromBody] ModelRequest<TodoItemJson> request)
        {
            UserBase user =null;
            // 参数检查 这里需要改一下 先看ID，在数据库中有则看密码，密码错误返回，再看用户名，在数据库中有则看密码，密码错误返回，无则新建用户，需要返回ID？？
            if (string.IsNullOrWhiteSpace(request.User.Name) && string.IsNullOrWhiteSpace(request.User.Id))
            {
                response.Code = ResponseDefine.BadRequset;
                response.Message += "用户Id和用户名不能同时为空";
                return;
            }
            else
            {
                // 用户信息有效性验证
                // 查看用户是否存在
                if (string.IsNullOrWhiteSpace(request.User.Name))
                {
                    user = await UserBaseStore.ById("System", request.User.Id).SingleOrDefaultAsync();
                    if (user == null)
                    {
                        response.Code = ResponseDefine.BadRequset;
                        response.Message += "用户不存在";
                    }
                    if (user.Pwd != request.User.Pwd)
                    {
                        response.Code = ResponseDefine.BadRequset;
                        response.Message += "密码错误";
                    }
                } else
                {
                    user = await UserBaseStore.ByName("System", request.User.Name).SingleOrDefaultAsync();
                    // 不存在则在数据库中创建一个
                    if (user == null)
                    {
                        string uid = Guid.NewGuid().ToString();
                        UserBase userBase = new UserBase
                        {
                            Id = uid,
                            Name = request.User.Name,
                            Pwd = request.User.Pwd,
                            _CreateUserId = uid,
                            _IsDeleted = false
                        };
                        await UserBaseStore.Create(user);
                    }
                    if (user.Pwd != request.User.Pwd)
                    {
                        response.Code = ResponseDefine.BadRequset;
                        response.Message += "密码错误";
                    }
                }
            }
            if (request.model == null)
            {
                response.Code = ResponseDefine.BadRequset;
                response.Message += "待办项为空，无法执行操作";
            }
            else
            {
                // 不存在ID，创建操作
                if (string.IsNullOrWhiteSpace(request.model.Id))
                {
                    TodoItem todo = await Store.Create(new TodoItem
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = request.model.Name,
                        _CreateUserId = user.Id
                    }, default(CancellationToken));
                    response.Extension = Mapper.Map<TodoItemJson>(todo);
                    response.Message += "新建待办项成功";
                }
                // 待办项ID存在，更新操作
                else
                {
                    // Id 是否有效
                    TodoItem todo = await Store.ById(user.Id, request.model.Id).SingleOrDefaultAsync();
                    if (todo == null)
                    {
                        response.Code = ResponseDefine.NotFound;
                        response.Message += "找不到该待办项";
                    }
                    else
                    {
                        response.Extension = Mapper.Map<TodoItemJson>(await Store.Update(todo, default(CancellationToken)));
                        response.Message += "更新待办项成功";
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        public void Delete([Required] ResponseMessage<TodoItemJson> response, [FromBody] ModelRequest<TodoItemJson> request)
        {
            // 删除关联和待办项
        }

        public void List([Required] ResponseMessage<List<TodoItemJson>> response, [FromBody] ModelRequest<TodoItemJson> request)
        {
            
        }
    }
}
