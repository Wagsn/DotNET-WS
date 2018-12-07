using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Blog.Models;
using WS.Blog.Stores;

namespace WS.Blog.Managers
{
    /// <summary>
    /// 博客管理
    /// </summary>
    public class BlogManager : IBlogManager
    {
        /// <summary>
        /// 博客存储
        /// </summary>
        public IBlogStore BlogStore { get; set; }

        /// <summary>
        /// 将存储注入到管理
        /// </summary>
        /// <param name="store"></param>
        public BlogManager(IBlogStore store)
        {
            BlogStore = store;
        }

        /// <summary>
        /// 通过ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.Blog ById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="blog"></param>
        public void Create(Models.Blog blog)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="blog"></param>
        public void Delete(Models.Blog blog)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="blog"></param>
        public void Update(Models.Blog blog)
        {
            throw new NotImplementedException();
        }
    }
}
