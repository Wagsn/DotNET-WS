using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WS.Todo.Models;

namespace WS.Todo.Stores
{
    /// <summary>
    /// 需要追踪的统一接口
    /// </summary>
    public interface ITodoItemStore<TContext, TModel> where TContext : ApplicationDbContext where TModel : TodoItem
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        TContext Context { get; set; }

        /// <summary>
        /// 模型类型
        /// </summary>
        Type ModelType { get; set; }

        /// <summary>
        /// 通过GUID查询待办项
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <param name="id">待办项Id</param>
        /// <returns></returns>
        IQueryable<TodoItem> ById([Required]string userid, [Required]string id);

        /// <summary>
        /// 批量查询，通过表达式筛选
        /// </summary>
        /// <param name="query">查询函数</param>
        /// <returns></returns>
        IQueryable<TResult> List<TResult>([Required]string userid, [Required]Func<IQueryable<TodoItem>, IQueryable<TResult>> query);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name = "todoItem">用户Id</param>
        /// <param name="cancellationToken">是否取消</param>
        /// <returns></returns>
        Task<TodoItem> Create([Required]TodoItem todoItem, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="todoItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TodoItem> Update([Required]TodoItem todoItem, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 批量删除，通过Id
        /// </summary>
        /// <param name="ids">批量待办项</param>
        /// <param name="cancellationToken">是否取消</param>
        /// <returns></returns>
        Task DeleteAll([Required]string userid, [Required]List<string> ids, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 条件删除，如果GUID等于
        /// </summary>
        /// <param name="id">待办GUID</param>
        /// <param name="cancellationToken">是否取消</param>
        /// <returns></returns>
        Task DeleteIfId([Required]string userid, [Required]string id, CancellationToken cancellationToken =default(CancellationToken));
    }
}
