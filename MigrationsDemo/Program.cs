using Microsoft.Extensions.Configuration;
using System;

namespace MigrationsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //// 读取配置文件
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("./cfg/config.json")
            //    .AddEnvironmentVariables()
            //    .AddCommandLine(args)
            //    .Build();

            using (BlogContext db = new BlogContext())
            {
                db.Blogs.Add(new Blog { Name = "Another Blog" });
                db.SaveChanges();

                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine(blog.Name);
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
