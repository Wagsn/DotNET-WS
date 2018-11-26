using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using WS.Music.Models;

using AutoMapper;

namespace WS.Music
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 配置文件
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables()
                .Build();
            // 配置数据库服务
            // Pomelo.EntityFrameworkCore.MySql  Dapper
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration["Data:DefaultConnection:ConnectionString"]);
                //options.UseOpenIddict();
            });
            
            // 这里应该是依赖注入
            services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // 这个方法是自定义的 IServiceCollectionExtensions.cs
            services.AddUserDefined();

            //AutoMapper.Mapper.

            //Mapper.Initialize(x => x.AddProfile<MappingProfile>());

            //services.AddScoped<IMapper, Mapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseMvc();

            // Call the Web API with jQuery
            // 默认文件夹访问index.html
            app.UseDefaultFiles();
            // 让外部可以访问wwwroot文件夹
            app.UseStaticFiles();
        }
    }
}
