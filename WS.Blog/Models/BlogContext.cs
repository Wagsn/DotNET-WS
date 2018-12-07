using Microsoft.EntityFrameworkCore;

namespace WS.Blog.Models
{
    /// <summary>
    /// 博客的数据库上下文
    /// </summary>
    public class BlogContext : DbContext
    {
        #region << 数据集 >>
        /// <summary>
        /// 博客
        /// </summary>
        public DbSet<Blog> Blogs { get; set; }
        #endregion

        /// <summary>
        /// 当进行数据库配置的时候
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=ws_test;user=admin;password=123456;");
        }

        /// <summary>
        /// 当模型被创建的时候，在这里进行模型于数据库表的映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 将Blog映射到数据库，BlogId作为索引
            modelBuilder.Entity<Blog>().HasIndex(u => u.BlogId).IsUnique();  // BlogId 是唯一的
        }
    }
}
