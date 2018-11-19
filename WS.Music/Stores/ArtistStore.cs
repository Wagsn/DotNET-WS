using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WS.Core.Stores;
using WS.Music.Models;

namespace WS.Music.Stores
{
    public class ArtistStore : StoreBase<ApplicationDbContext, Artist>
    {
        public ArtistStore(ApplicationDbContext context) : base(context) { }

        public override Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<Artist>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过名称找到Artist，模糊查询
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IQueryable<Artist> ByName(string Name)
        {
            var query = from a in Context.Artists
                        where a.Name.Contains(Name)
                        select new Artist(a);
            return query;
        }
    }
}
