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
    /// 歌曲存储，写一些特殊的数据库存取操作
    /// </summary>
    public class SongStore : StoreBase<ApplicationDbContext, Song>
    {
        /// <summary>
        /// 歌曲存储
        /// </summary>
        /// <param name="context"></param>
        public SongStore(ApplicationDbContext context) : base(context) { }

        /// <summary>
        /// 批量查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<Song>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            CheckNull(query);

            return query.Invoke(Context.Songs.Where(i => i._IsDeleted == false).AsNoTracking()).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 创建或更新
        /// </summary>
        public void CreateOrUpdate() { }
    }
}
