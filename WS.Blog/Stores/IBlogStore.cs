using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Blog.Stores
{
    /// <summary>
    /// 博客存储的接口
    /// </summary>
    public interface IBlogStore
    {
        /// <summary>
        /// 创建模型，以后调用这个方法之后会在数据库Blog表中增加一条记录
        /// </summary>
        /// <param name="blog"></param>
        void Create(Models.Blog blog);

        /// <summary>
        /// 更新模型
        /// </summary>
        /// <param name="blog"></param>
        void Update(Models.Blog blog);

        /// <summary>
        /// 删除模型
        /// </summary>
        /// <param name="blog"></param>
        void Delete(Models.Blog blog);

        /// <summary>
        /// 查询模型
        /// </summary>
        /// <param name="id">Blog的ID</param>
        /// <returns></returns>
        Models.Blog ById(int id);
    }
}
