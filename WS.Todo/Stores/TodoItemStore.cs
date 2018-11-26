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
    public class TodoItemStore : ITodoItemStore<ApplicationDbContext, TodoItem>
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
            // 查询User包含的Todo
            var todoIds = from rut in Context.RelationUserTodos
                          where rut.UserId == userid && rut.TodoId == id
                          select rut.TodoId;
            return List(userid, a => a.Where(b => todoIds.Contains(b.Id)));
        }

        /// <summary>
        /// 批量获取待办项（过滤IsDeleted==true），通用函数
        /// 用户数据隔离
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="userid">用户ID</param>
        /// <param name="query">查询</param>
        /// <returns></returns>
        public IQueryable<TResult> List<TResult>([Required]string userid, [Required]Func<IQueryable<TodoItem>, IQueryable<TResult>> query)
        {
            // 软删除，过滤
            return query.Invoke(Context.TodoItems.Where(it => it._CreateUserId==userid&&it._IsDeleted==false));
        }

        /// <summary>
        /// 创建待办项
        /// </summary>
        /// <param name="todoItem">待办项</param>
        /// <param name="cancellationToken">是否取消</param>
        /// <returns></returns>
        public async Task<TodoItem> Create([Required] TodoItem todoItem, CancellationToken cancellationToken = default(CancellationToken))
        {
            // 创建的时候必须保证创建人ID不能为空
            if (todoItem._CreateUserId==null)
            {
                throw new ArgumentNullException("用户ID不能为空");
            }
            todoItem.ActualTime = null;
            todoItem.IsComplete = false;
            todoItem._CreateTime = DateTime.Now;
            todoItem._IsDeleted = false;
            Context.Add(todoItem);
            Context.Add(new RelationUserTodo
            {
                UserId = todoItem._CreateUserId,  // 确保 UserId 不为空
                TodoId = todoItem.Id
            });
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
            // TODO 判断UserTodo关联
            //var usertodo =Context.RelationUserTodos.Where(a => a.UserId == todoItem._CreateUserId && a.TodoId == todoItem.Id);
            //if (usertodo == null)
            //{
            //    return null;
            //}  // 按理说在创建的时候已经关联了的，而已删除的todoitem客户端接收不到，所以传入的todoitem应该是合法数据
            var currTime = DateTime.Now;
            todoItem._UpdateTime = currTime;
            Context.Attach(todoItem);
            var item =Context.Update(todoItem).Entity;
            // 添加变更历史
            TodoItemHistory history = new TodoItemHistory
            {
                Id = Guid.NewGuid().ToString(),
                UserId = item._UpdateUserId,
                Type = "Delete",  // 放到常量池
                Time = currTime,
                Content = JsonHelper.ToJson(item)
            };
            Context.Add(history);
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
            // 找到所有关联的未删除的待办
            var query = List(userid, a => a.Where(b => b.Id == id));

            DateTime currTime = DateTime.Now;
            foreach(var item in query)
            {
                // 软删除去掉，改为变更历史 TodoItemHistory
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
                // 删除UserTodo关联
                Context.Remove(Context.RelationUserTodos.Where(a => a.UserId == userid && a.TodoId == id));
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
            // 删除UserTodo关联
            Context.Remove(Context.RelationUserTodos.Where(a => a.UserId == userid && query.Select(i=> i.Id).Contains(a.TodoId)));
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
