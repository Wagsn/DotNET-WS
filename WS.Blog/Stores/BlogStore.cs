using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Blog.Models;

namespace WS.Blog.Stores
{
    /// <summary>
    /// 博客存储实现
    /// </summary>
    public class BlogStore : IBlogStore
    {
        /// <summary>
        /// 数据库上下文，使用它来进行数据库操作
        /// </summary>
        public BlogContext Context { get; set; }

        /// <summary>
        /// 构造器，在这里进行依赖注入，对于数据库上下文来说，系统会自己帮你注入（所谓注入就是你在使用Store的时候会帮你生成数据库上下文对象）
        /// </summary>
        /// <param name="context"></param>
        public BlogStore(BlogContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 通过ID查询博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.Blog ById(int id)
        {
            // 这里使用到LINQ，可以了解一下，用来查询很方便的
            // query 是符合条件的Blog的集合
            var query = from blog in Context.Blogs
                        where blog.BlogId == id
                        select blog;
            // 我们取一个
            return query.FirstOrDefault();
        }

        /// <summary>
        /// 创建博客
        /// </summary>
        /// <param name="blog"></param>
        public void Create(Models.Blog blog)
        {
            // 在数据中添加
            Context.Add(blog);

            try
            {
                // 之前那个Add是一个缓存，只有执行了这一句之后才是真的在数据库中保存起来了
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Blog 添加失败，在BlogStore的Create方法中，" + e);
            }
        }

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="blog"></param>
        public void Delete(Models.Blog blog)
        {
            Context.Remove(blog);
            try
            {
                // 之前那个是一个缓存，只有执行了这一句之后才是真的在数据库中进行了操作
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Blog 添加失败，在BlogStore的Create方法中，" + e);
            }
        }

        /// <summary>
        /// 更新博客
        /// </summary>
        /// <param name="blog"></param>
        public void Update(Models.Blog blog)
        {
            Context.Update(blog);

            try
            {
                // 之前那个是一个缓存，只有执行了这一句之后才是真的在数据库中进行了操作
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Blog 添加失败，在BlogStore的Create方法中，" + e);
            }
        }
    }
}
