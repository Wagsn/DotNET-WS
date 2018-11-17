using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Stores;
using WS.Music.Models;

namespace WS.Music.Stores
{
    /// <summary>
    /// 消息存储
    /// </summary>
    public class MessageStore : StoreBase<ApplicationDbContext, Message>
    {
        public MessageStore(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// 批量查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<Message>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            CheckNull(query);

            return query.Invoke(Context.Messages.Where(i => i._IsDeleted == false).AsNoTracking()).ToListAsync(cancellationToken);
        }
    }
}
