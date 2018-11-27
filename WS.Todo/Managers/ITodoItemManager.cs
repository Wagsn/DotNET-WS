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
    /// 待办项管理
    /// </summary>
    public interface ITodoItemManager<TStore> where TStore : ITodoItemStore<ApplicationDbContext, TodoItem>
    {
        /// <summary>
        /// 待办项存储
        /// </summary>
        TStore Store { get; set; }

        /// <summary>
        /// 获取该用户所有待办，通过用户ID
        /// </summary>
        /// <param name="response">响应体</param>
        /// <param name="request">请求体</param>
        /// <param name="cancellationToken">是否取消</param>
        /// <returns></returns>
        Task List([Required]PagingResponseMessage<TodoItemJson> response, [FromBody]PageRequest request, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 创建当没有Id时，更新当有Id时
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken">是否取消</param>
        /// <returns></returns>
        Task CreateOrUpdate([Required]ResponseMessage<TodoItemJson> response, [FromBody]ModelRequest<TodoItemJson> request, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 删除，通过用户ID和待办ID
        /// </summary>
        /// <param name="response"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken">是否取消</param>
        /// <returns></returns>
        Task Delete([Required]ResponseMessage<TodoItemJson> response, [FromBody]ModelRequest<TodoItemJson> request, CancellationToken cancellationToken = default(CancellationToken));
    }
}
