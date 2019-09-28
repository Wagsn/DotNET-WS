using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WS.FileServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

            var host = WebHost.CreateDefaultBuilder(args)
                // 使用配置文件
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    // 所有的controller都不限制post的body大小
                    options.Limits.MaxRequestBodySize = null;
                })
                // 设置端口（*表示主机地址）
                .UseUrls($"http://*:{config["Port"]}")
                .Build();

            host.Run();
        }
    }
}
