using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FileServer
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
            services.AddSingleton(Configuration.GetSection("FileServer").Get<FileServerConfig>());

            //设置接收文件长度的最大值。
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowCredentials();
            });
            #region 文件上传
            FileServerConfig config = app.ApplicationServices.GetService<FileServerConfig>();
            if (config != null && config.PathList != null)
            {
                List<PathItem> pathList = new List<PathItem>();
                foreach (PathItem pi in config.PathList)
                {
                    if (string.IsNullOrEmpty(pi.LocalPath))
                        continue;

                    try
                    {
                        if (!System.IO.Directory.Exists(pi.LocalPath))
                        {
                            System.IO.Directory.CreateDirectory(pi.LocalPath);
                        }
                        pathList.Add(pi);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("文件夹创建失败：\r\n{0}", e.ToString());
                    }
                }
                if (config.Root != null)
                {
                    if (!System.IO.Directory.Exists(config.Root.LocalPath))
                    {
                        System.IO.Directory.CreateDirectory(config.Root.LocalPath);
                    }
                    pathList.Add(config.Root);
                }

                foreach (PathItem pi in pathList)
                {
                    Console.WriteLine("Full Path: " + System.IO.Path.GetFullPath(pi.LocalPath));
                    app.UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.GetFullPath(pi.LocalPath)),
                        RequestPath = pi.Url
                    });
                    Console.WriteLine("路径映射：{0}-->{1}", pi.LocalPath, pi.Url);
                }
            }
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
