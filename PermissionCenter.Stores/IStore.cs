using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PermissionCenter.Stores
{
    /// <summary>
    /// 存储器接口
    /// </summary>
    public interface IStore<TEntity> where TEntity:class
    {
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> CreateAsync(TEntity entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity);
    }
}
