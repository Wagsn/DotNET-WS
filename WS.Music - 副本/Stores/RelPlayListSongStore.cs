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
    public class RelPlayListSongStore : StoreBase<ApplicationDbContext, RelPlayListSong>
    {
        public RelPlayListSongStore(ApplicationDbContext context) : base(context) {}

        /// <summary>
        /// 查询歌单与歌曲的关联实体
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<RelPlayListSong>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            CheckNull(query);

            /// TODO: <see cref="ApplicationDbContext.Models{TResult}"/>
            return query.Invoke(Context.RelPlayListSongs.Where(i => i._IsDeleted == false).AsNoTracking()).ToListAsync(cancellationToken);
        }
    }
}
