using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace WS.Todo.Models
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){ }

        /// <summary>
        /// 待办项
        /// </summary>
        public DbSet<TodoItem> TodoItems { get; set; }

        /// <summary>
        /// 待办项历史变更
        /// </summary>
        public DbSet<TodoItemHistory> TodoItemHistories { get; set; }
        
        /// <summary>
        /// 用户核心信息，扩展信息只与Id挂钩，不与Name和Pwd挂钩
        /// </summary>
        public DbSet<UserBase> UserBases { get; set; }

        /// <summary>
        /// 用户待办关联
        /// </summary>
        public DbSet<RelationUserTodo> RelationUserTodos { get; set; }

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
                b.Property(p=>p._IsDeleted);
            });

            builder.Entity<TodoItemHistory>(b =>
            {
                b.ToTable("ws_todo_todoitem_history");
            });

            builder.Entity<UserBase>(b =>
            {
                b.ToTable("ws_todo_userbase");
                b.Property(p => p._IsDeleted);
            });

            builder.Entity<RelationUserTodo>(b =>
            {
                b.ToTable("ws_todo_relation_usertodo").HasKey(k => new { k.TodoId, k.UserId });
            });
        }
    }
}
