using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Stores
{
    /// <summary>
    /// 事务接口
    /// </summary>
    public interface ITransaction
    {
        Task<IDbContextTransaction> BeginTransaction();
    }
}
