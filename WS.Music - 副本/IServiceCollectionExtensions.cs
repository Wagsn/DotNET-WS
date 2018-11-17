using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Music.Managers;
using WS.Music.Models;
using WS.Music.Stores;

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

            services.AddScoped<ITransaction, Transaction<ApplicationDbContext>>();
            //services.AddTransient<IMapper>();

            #region << Store >>

            services.AddTransient<SongStore>();
            services.AddTransient<UserStore>();
            services.AddTransient<SendStore>();
            services.AddTransient<MessageStore>();
            services.AddTransient<RelPlayListSongStore>();
            services.AddTransient<RelUserPlayListStore>();

            #endregion
            
            #region << Manager >>

            services.AddTransient<UserManager>();
            services.AddTransient<SongManager>();
            services.AddTransient<SendManager>();

            #endregion

            #endregion

            return new UserDefinedBuilder(services);
        }
    }
}
