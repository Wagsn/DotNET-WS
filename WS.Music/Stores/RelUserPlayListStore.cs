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
    public class RelUserPlayListStore : StoreBase<ApplicationDbContext, RelUserPlayList>
    {
        public RelUserPlayListStore(ApplicationDbContext context) : base(context) {}

        /// <summary>
        /// 批量查询用户歌单关联
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<RelUserPlayList>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            CheckNull(query);
            
            /// TODO: <see cref="ApplicationDbContext.Models{TResult}"/>
            return query.Invoke(Context.RelUserPlayLists.Where(i => i._IsDeleted == false).AsNoTracking()).ToListAsync(cancellationToken);
        }
    }
}
