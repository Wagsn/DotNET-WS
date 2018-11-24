using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WS.Core.Text;
using WS.Todo.Models;

namespace WS.Todo.Stores
{
    /// <summary>
    /// 用户核心信息存储实现
    /// </summary>
    public class UserBaseStore : IUserBaseStore<ApplicationDbContext, UserBase>
    {
        public ApplicationDbContext Context { get; set; }

        public Type ModelType { get; set; }

        public IQueryable<UserBase> ById([Required]string userid, [Required]string id)
        {
            return List(userid, a => a.Where(b => b.Id == id));
        }

        public IQueryable<UserBase> ByName([Required]string userid, [Required]string name)
        {
            return List(userid, a => a.Where(b => b.Name == name)); // userid= System代表系统 Self代表自己
        }

        public IQueryable<UserBase> List([Required]string userid, [Required]Func<IQueryable<UserBase>, IQueryable<UserBase>> query)
        {
            return query.Invoke(Context.UserBases.Where(ub => !ub._IsDeleted));
        }

        public async Task<UserBase> Create([Required] UserBase user, CancellationToken cancellationToken = default(CancellationToken))
        {
            user._CreateTime = DateTime.Now;
            user._IsDeleted = false;
            Context.Add(user);
            // 添加变更历史
            //TodoItemHistory history = new TodoItemHistory
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    UserId = user._CreateUserId,
            //    Type = "Create",  // 放到常量池
            //    Time = DateTime.Now,
            //    Content = JsonHelper.ToJson(user)
            //};
            //Context.Add(history);
            try
            {
                await Context.SaveChangesAsync(cancellationToken);
                return user;
            }
            catch (DbUpdateConcurrencyException)
            {
                // log
                throw;
            }
        }

        public async Task<UserBase> Update([Required] UserBase user, CancellationToken cancellationToken = default(CancellationToken))
        {
            Context.Attach(user);
            var item = Context.Update(user).Entity;

            try
            {
                await Context.SaveChangesAsync(cancellationToken);
                return user;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
