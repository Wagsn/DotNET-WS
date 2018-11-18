using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Swagger;

using WS.Todo.Models;
using WS.Todo.Stores;

namespace WS.Todo
{
    /// <summary>
    /// 启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 启动器构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
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

            //services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));  // Register the database context
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //var connection = @"Server=(localdb)\mssqllocaldb;Database=TodoApi;Trusted_Connection=True;ConnectRetryCount=0";
            //services.AddDbContext<ApplicationDbContext>
            //    (options => options.UseSqlServer(connection));

            
            
            // 依赖注入
            //services.AddTransient<IStore<TodoItem>, TodoItemStore>();
            services.AddTransient<TodoItemStore>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "待办接口文档",
                    TermsOfService = "None",
                });
                c.IgnoreObsoleteActions();
                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "api.xml");
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // 配置Swagger中间件，必须在UseMvc之前
            // 启用Swagger中间件以生成Swagger作为JSON数据（在app.UseMvc();之前）
            app.UseSwagger();
            app.UseSwaggerUI(c=> 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WS TODO API V1");
                //c.ShowRequestHeaders();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    // HTTPS   HSTS 中间件 (UseHsts) 发送给客户端的 HTTP 严格传输安全性协议 (HSTS) 标头。
            //    app.UseHsts();
            //}

            // 启用https协议
            //app.UseHttpsRedirection();
            app.UseMvc();

            // Call the Web API with jQuery
            // 默认访问index.html
            app.UseDefaultFiles();
            // 允许访问wwwroot
            app.UseStaticFiles();

            // 数据库初始化
        }
    }
}
