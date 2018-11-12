using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using TodoApi.Models;

namespace TodoApi.Stores
{
    /// <summary>
    /// 未使用
    /// </summary>
    public interface ITodoItemStore: IStore<TodoItem>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="todoItem"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //Task<TodoItem> CreateAsync(TodoItem todoItem, CancellationToken cancellationToken);

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //Task<TResult> GetAsync<TResult>(Func<IQueryable<TodoItem>, IQueryable<TResult>> query, CancellationToken cancellationToken);

        /// <summary>
        /// 批量查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<TodoItem>, IQueryable<TResult>> query, CancellationToken cancellationToken);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="TodoItemList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //Task DeleteListAsync(List<TodoItem> TodoItemList, CancellationToken cancellationToken);
    }
}
