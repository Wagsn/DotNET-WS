using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using WS.Todo.Dto;
using WS.Todo.Models;

namespace WS.Todo.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public class TodoItemStore : StoreBase<ApplicationDbContext, TodoItem>
    {
        public TodoItemStore(ApplicationDbContext context) : base(context)
        {
            //Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        //protected override ApplicationDbContext Context => base.Context;

        public async override Task DeleteIfAsync(Func<IQueryable<TodoItem>, IQueryable<TodoItem>> query, CancellationToken cancellationToken)
        {
            Util.CheckNull(query);

            // 软删除
            // 找到要删除的
            var models = query.Invoke(Context.TodoItems).ToList();
            models.ForEach(model=> 
            {
                model.DeleteTime = DateTime.Now;
                model.IsDeleted = true;
            });
            Context.UpdateRange(models);

            // 硬删除
            //Context.Remove(models);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public override Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<TodoItem>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            Util.CheckNull(query);

            return query.Invoke(Context.TodoItems.Where(i => i.IsDeleted==false).AsNoTracking()).ToListAsync(cancellationToken);
        }

        public override Task<TResult> ReadAsync<TResult>(Func<IQueryable<TodoItem>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            Util.CheckNull(query);

            return query.Invoke(Context.TodoItems.Where(i => i.IsDeleted == false).AsNoTracking()).SingleOrDefaultAsync(cancellationToken);
        }

        public async override  Task<TodoItem> ReadAsync(TodoItem model, CancellationToken cancellationToken)
        {
            return await ReadAsync(a => a.Where(b => b.Id == model.Id), cancellationToken);
        }

        // 未完成
        public async override Task UpdateIfAsync(Func<IQueryable<TodoItem>, IQueryable<TodoItem>> query, CancellationToken cancellationToken)
        {
            Util.CheckNull(query);

            var models = query.Invoke(Context.TodoItems.Where(i => i.IsDeleted == false));  // 修改后的models

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return;
        }
    }
}
