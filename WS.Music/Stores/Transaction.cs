﻿using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Music.Models;

namespace WS.Music.Stores
{
    public class Transaction<TContext> : ITransaction where TContext : ApplicationDbContext
    {
        private readonly ApplicationDbContext dbContext;
        public Transaction(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await dbContext.Database.BeginTransactionAsync();
        }
    }
}
