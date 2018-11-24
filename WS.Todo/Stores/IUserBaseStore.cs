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
    /// 用户核心信息存储接口
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public interface IUserBaseStore<TContext, TModel> where TContext : DbContext where TModel : UserBase
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
        /// 通过Id查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IQueryable<TModel> ById([Required]string userid, [Required]string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IQueryable<TModel> ByName([Required]string userid, [Required]string name);

        /// <summary>
        /// 批量查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryable<TModel> List([Required]string userid, [Required]Func<IQueryable<TModel>, IQueryable<TModel>> query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TModel> Create([Required]TModel user, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TModel> Update([Required]TModel user, CancellationToken cancellationToken = default(CancellationToken));
    }
}
