using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using WS.Core.Text;
using WS.Todo.Dto;
using WS.Todo.Models;

namespace WS.Todo.Stores
{
    /// <summary>
    /// 待办项存储
    /// </summary>
    public class TodoItemStore : ITodoItemStore<ApplicationDbContext, Models.TodoItem>
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public ApplicationDbContext Context { get; set; }

        public Type ModelType { get; set; }

        /// <summary>
        /// 构造器，注入ApplicationDbContext
        /// </summary>
        /// <param name="context"></param>
        public TodoItemStore(ApplicationDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ModelType = typeof(TodoItem);
        }

        /// <summary>
        /// 通过GUID查询待办项
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<TodoItem> ById([Required]string userid, [Required]string id)
        {
            return List(userid, a=>a.Where(b=>b.Id==id));
        }

        /// <summary>
        /// 批量获取待办项（过滤IsDeleted==true），通用函数
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="userid"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<TResult> List<TResult>([Required]string userid, [Required]Func<IQueryable<TodoItem>, IQueryable<TResult>> query)
        {
            // 软删除，过滤
            return query.Invoke(Context.TodoItems.Where(it => !it._IsDeleted));
        }

        /// <summary>
        /// 创建待办项
        /// </summary>
        /// <param name="todoItem">待办项</param>
        /// <param name="cancellationToken">是否取消</param>
        /// <returns></returns>
        public async Task<TodoItem> Create([Required] TodoItem todoItem, CancellationToken cancellationToken = default(CancellationToken))
        {
            todoItem.IsComplete = false;
            todoItem._CreateTime = DateTime.Now;
            todoItem._IsDeleted = false;
            Context.Add(todoItem);
            // 添加变更历史
            TodoItemHistory history = new TodoItemHistory
            {
                Id = Guid.NewGuid().ToString(),
                UserId = todoItem._CreateUserId,
                Type = "Create",  // 放到常量池
                Time = DateTime.Now,
                Content = JsonHelper.ToJson(todoItem)
            };
            Context.Add(history);
            try
            {
                await Context.SaveChangesAsync(cancellationToken);
                return todoItem;
            }
            catch (DbUpdateConcurrencyException)
            {
                // log
                throw;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="todoItem">待办项</param>
        /// <param name="cancellationToken">是否取消</param>
        /// <returns></returns>
        public async Task<TodoItem> Update([Required] TodoItem todoItem, CancellationToken cancellationToken)
        {
            Context.Attach(todoItem);
            var item =Context.Update(todoItem).Entity;

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
                return item;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过GUID删除待办项
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DeleteIfId([Required]string userid, [Required]string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = List(userid, (IQueryable<TodoItem> a) => a.Where(b => b.Id == id));

            DateTime currTime = DateTime.Now;
            foreach(var item in query)
            {
                item._DeleteUserId = userid;
                item._DeleteTime = currTime;
                item._IsDeleted = true;
                // 添加变更历史
                TodoItemHistory history = new TodoItemHistory
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userid,
                    Type = "Delete",  // 放到常量池
                    Time = currTime,
                    Content = JsonHelper.ToJson(item)
                };
                Context.Add(history);
            }
            // 软删除
            Context.UpdateRange(query);

            //// 硬删除
            ////Context.Remove(models);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除所有
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DeleteAll([Required]string userid, [Required]List<string> ids, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = List(userid, a => a.Where(b => ids.Contains(b.Id)));
            DateTime currTime = DateTime.Now;
            foreach (var item in query)
            {
                item._DeleteUserId = userid;
                item._DeleteTime = currTime;
                item._IsDeleted = true;
                // 添加变更历史
                TodoItemHistory history = new TodoItemHistory
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userid,
                    Type = "Delete",  // 常量池
                    Time = currTime,
                    Content = JsonHelper.ToJson(item)
                };
                Context.Add(history);
            }
            // 软删除
            Context.UpdateRange(query);

            //// 硬删除
            ////Context.Remove(models);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
