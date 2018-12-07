using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Blog.Managers
{
    /// <summary>
    /// 博客管理
    /// </summary>
    public interface IBlogManager
    {
        /// <summary>
        /// 创建博客
        /// </summary>
        /// <param name="blog"></param>
        void Create(Models.Blog blog);

        /// <summary>
        /// 更新博客
        /// </summary>
        /// <param name="blog"></param>
        void Update(Models.Blog blog);

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="blog"></param>
        void Delete(Models.Blog blog);

        /// <summary>
        /// 通过Id查询博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Models.Blog ById(int id);
    }
}
