using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PermissionCenter.Stores
{
    /// <summary>
    /// 存储器
    /// </summary>
    public abstract class Store<TEntity, TDbContext> : IStore<TEntity> where TEntity : class where TDbContext : DbContext
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private TDbContext DbContext { get; set; }

        public Store(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            DbContext.Add(entity);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            DbContext.Remove(entity);
            return await DbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null)
        {
            if(predicate == null)
            {
                return DbContext.Set<TEntity>().AsNoTracking();
            }
            else
            {
                return DbContext.Set<TEntity>().AsNoTracking().Where(predicate);
            }
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            DbContext.Attach(entity);
            DbContext.Update(entity);
            return await DbContext.SaveChangesAsync();
        }
    }
}
