using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionCenter.Dto
{
    public abstract class BaseResponse
    {

    }

    public class TokenResponseMessage : ResponseMessage
    {
        public string access_token { get; set; }
    }


    public class ResponseMessage : BaseResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ResponseMessage()
        {
            Code = "0";
        }

        public bool IsSuccess() => Code == "0";
    }

    public class ResponseMessage<TEx> : ResponseMessage
    {
        public TEx Extension { get; set; }
    }

    public class PagingResponseMessage<TEntity> : ResponseMessage<List<TEntity>>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public long TotalCount { get; set; }

        public int PageCount { get => TotalCount == 0 ? 0 : (int)Math.Ceiling(PageSize / (double)PageCount); }

        public async Task<PagingResponseMessage<TEntity>> HandleData(IQueryable<TEntity> query, int index, int size, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryData = await query.Skip(index * size).Take(size).ToListAsync(cancellationToken);
            return new PagingResponseMessage<TEntity>
            {
                Extension = queryData,
                PageIndex = index,
                PageSize = size,
                TotalCount = await query.LongCountAsync(cancellationToken),
            };
        }

        public async Task<PagingResponseMessage<TEntity>> WrapData<TSource>(IQueryable<TSource> query, Expression<Func<TSource, TEntity>> selector, int index, int size, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryData = await query.Select(selector).Skip(index * size).Take(size).ToListAsync(cancellationToken);
            return new PagingResponseMessage<TEntity>
            {
                Extension = queryData,
                PageIndex = index,
                PageSize = size,
                TotalCount = await query.LongCountAsync(cancellationToken),
            };
        }
    }
}
