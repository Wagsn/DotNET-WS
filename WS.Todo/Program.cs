using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using WS.Log;
using ILogger = WS.Log.ILogger;

namespace WS.Todo
{
    public class Program
    {
        //
        public static ILogger Logger = LoggerManager.GetLogger("Program");

        public static void Main(string[] args)
        {
            // CreateWebHostBuilder(args).Build().Run();

            // 读取配置文件
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("./cfg/config.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            // 设置日志工具的配：输出路径与文件大小

            bool isLog = false;
            string logOut = "./log";

            var logConfig = configuration.GetSection("WSLog");
            if (logConfig != null)
            {
                isLog = bool.TryParse(logConfig["IsLog"], out isLog);
                if (isLog)
                {
                    logOut = logConfig["LogOut"];
                    // ...
                    LogConfig logcfg = new LogConfig
                    {
                        IsLog= isLog,
                        LogOut = logOut
                    };
                }
            }

            // 配置文件设置端口号，检查是否符合端口规范，默认端口 5000
            string port = configuration["Port"] ?? "5000";
            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://*:{port}")
                .Build();
            Logger.Info($"Web Host start, The port is {port}, if you want to change it, you can modify configuration file in projectroot/cfg/config.json");
            host.Run();

        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        //.UseUrls("http://*:56470")
        //        //.UseKestrel()
        //        //.UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseStartup<Startup>();

    }
}
