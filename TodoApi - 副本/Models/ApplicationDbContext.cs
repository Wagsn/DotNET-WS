using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace TodoApi.Models
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){ }

        public DbSet<TodoItem> TodoItems { get; set; }

        /// <summary>
        /// 模型创建
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TodoItem>(b =>
            {
                b.ToTable("ws_todo_todoitem");
                b.Property<bool?>("IsDeleted");
            });
        }
    }
}
