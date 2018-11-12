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
    public class UserStore : StoreBase<ApplicationDbContext, User>
    {
        public UserStore(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<User>, IQueryable<TResult>> query, CancellationToken cancellationToken)
        {
            CheckNull(query);

            return query.Invoke(Context.Users.Where(i => i._IsDeleted == false).AsNoTracking()).ToListAsync(cancellationToken);
        }
    }
}
