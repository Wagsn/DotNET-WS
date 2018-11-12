using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TodoApi.Models;
using WS.Core.Helpers;

namespace TodoApi.Stores
{
    /// <summary>
    /// Store基类，软删除在这里写，实现了一些通用方法，和工具方法
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public abstract class StoreBase<TContext, TModel> : IStore<TModel> where TContext : DbContext where TModel : TraceUpdateBase // IdentityDbContext
    {
        protected virtual TContext Context { get; }

        public StoreBase(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// 创建模型，TODO：如果数据库中已经存在一条已经删除的数据，现在需要重新创建，是否在原有数据上操作，以避免数据库的存储压力，比如歌名敏感问题，会将歌曲信息删除，等开放时再重新启用
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken)
        {
            CheckNull(model);

            // 添加时间
            model.CreateTime = DateTime.Now;
            model.IsDeleted = false;

            Context.Add(model);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            
            return model;

        }

        public async Task<List<TModel>> CreateListAsync(List<TModel> models, CancellationToken cancellationToken)
        {
            CheckNull(models);

            // 添加时间
            var currTime = DateTime.Now;
            models.ForEach(model =>
            {
                model.CreateTime = currTime;
                model.IsDeleted = false;
            });

            Context.AddRange(models);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return models;
        }

        public async Task DeleteAsync(TModel model, CancellationToken cancellationToken)
        {
            CheckNull(model);

            // 软删除
            model.DeleteTime = DateTime.Now;
            model.IsDeleted = true;
            Context.Update(model);

            // 硬删除
            //Context.Remove(model);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public abstract Task DeleteIfAsync(Func<IQueryable<TModel>, IQueryable<TModel>> query, CancellationToken cancellationToken);

        public async Task DeleteListAsync(List<TModel> models, CancellationToken cancellationToken)
        {
            CheckNull(models);

            // 软删除
            var currTime = DateTime.Now;
            models.ForEach(model =>
            {
                model.DeleteTime = currTime;
                model.IsDeleted = true;
            });
            Context.UpdateRange(models);

            // 硬删除
            //Context.RemoveRange(models);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public abstract Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<TModel>, IQueryable<TResult>> query, CancellationToken cancellationToken);

        public abstract Task<TModel> ReadAsync(TModel model, CancellationToken cancellationToken);

        public abstract Task<TResult> ReadAsync<TResult>(Func<IQueryable<TModel>, IQueryable<TResult>> query, CancellationToken cancellationToken);

        public async Task UpdateAsync(TModel model2, CancellationToken cancellationToken)
        {
            CheckNull(model2);

            var model = await ReadAsync(a => a.Where(b => model2.Equals(b)), cancellationToken);
            Console.WriteLine("WS------- StoreBase UpdateAsync 从数据库中获取的Model：\r\n"+JsonHelper.ToJson(model));
            model.Update(model2);
            
            // 更新时间
            model.UpdateTime = DateTime.Now;

            Context.Attach(model);
            Context.Update(model);

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public abstract Task UpdateIfAsync(Func<IQueryable<TModel>, IQueryable<TModel>> query, CancellationToken cancellationToken);

        public async Task UpdateList(List<TModel> models, CancellationToken cancellationToken)
        {
            CheckNull(models);

            // 更新时间
            var currTime = DateTime.Now;
            models.ForEach(model => 
            {
                model.UpdateTime = currTime;
            });

            Context.AttachRange(models);
            Context.UpdateRange(models);

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
        /// 空检查，暂时不知道参数注解（特性）怎么用
        /// </summary>
        /// <typeparam name="TArgument"></typeparam>
        /// <param name="arg"></param>
        protected void CheckNull<TArgument>(TArgument arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
        }
    }
}
