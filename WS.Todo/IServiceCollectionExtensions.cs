using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Todo.Managers;
using WS.Todo.Models;
using WS.Todo.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务容器扩展
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 在这里添加依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static UserDefinedBuilder AddUserDefined(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            #region << 在这里添加依赖注入 >>

            services.AddScoped<DbContext, ApplicationDbContext>();

            #region << Store >>

            services.AddScoped<ITodoItemStore<ApplicationDbContext, TodoItem>, TodoItemStore>();
            services.AddScoped <IUserBaseStore<ApplicationDbContext, UserBase>, UserBaseStore>();

            #endregion

            #region << Manager >>
            
            services.AddScoped<ITodoItemManager<ITodoItemStore<ApplicationDbContext, TodoItem>>, TodoItemManager>();

            #endregion

            #endregion

            return new UserDefinedBuilder(services);
        }
    }
}
