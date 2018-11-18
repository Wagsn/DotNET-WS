using System;

namespace WS.Shell
{
    class Program
    {
        ShellConfig Config { get; set; }

        ShellContext Context { get; set; }

        static void Main(string[] args)
        {
            // Wagsn Shell 的应用上下文
            ShellContext AppContext = new ShellContext();
            Console.Write($"{AppContext.CopyrightInfo}\r\n\r\nWS {AppContext.CurrentDirectory}> ");
            // 一个简单的控制台循环
            string nextLine = "";
            //bool hasOutput = true;
            while (true)
            {
                nextLine = Console.ReadLine().Trim();
                switch (nextLine)
                {
                    case "":
                        break;
                    case "id":
                        Console.WriteLine("id: "+AppContext.StartId);
                        break;
                    case "exit":
                        return;
                    case "clear":
                        Console.Clear();
                        //bool hasOutput = false;
                        break;
                    case "clearlast":
                        Console.SetCursorPosition(0, Console.CursorSize - 1);
                        break;
                    case "now":
                        Console.WriteLine(DateTime.Now.ToString(Define.Format.Time));
                        break;
                    case "help":
                        Console.WriteLine("[command]\t[decription]\r\nhelp\t\t帮助\r\nnow\t\t当前时间\r\nclear\t\t清除屏幕\r\nexit\t\t退出 Wagsn Shell\r\n\r\n");
                        break;
                    default:
                        Console.WriteLine("找不到关于 \"" + nextLine + "\" 的命令\r\n");
                        break;
                }
                //if (hasOutput) Console.WriteLine("WS < " + nextLine);
                //hasOutput = true;
                Console.Write($"WS {AppContext.CurrentDirectory}> ");
            }
        }

        /// <summary>
        /// 程序启动时使用
        /// </summary>
        void Startup(ShellConfig config)
        {
            Config = new ShellConfig
            {
                Context = new ShellContext()
            };
        }

        void MainHadle(string line)
        {
            switch (line)
            {
                case "clear":
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("找不到关于 \"" + line + "\"的命令");
                    break;
            }
        }
    }

    /// <summary>
    /// Wagsn Shell 配置，必须要可序列化
    /// </summary>
    public class ShellConfig
    {
        /// <summary>
        /// Wagsn Shell 应用上下文
        /// </summary>
        public ShellContext Context { get; set; }
    }

    /// <summary>
    /// Wagsn Shell Context
    /// </summary>
    public class ShellContext
    {

        public ShellConfig Config { get; set; }

        /// <summary>
        /// 应用当前路径
        /// </summary>
        public string CurrentDirectory { get => System.IO.Directory.GetCurrentDirectory(); set { } }

        /// <summary>
        /// 版权信息
        /// </summary>
        public string CopyrightInfo = "Wagsn Shell\r\n版权所有 (C) Wagsn。保留所有权利。";

        /// <summary>
        /// 程序启动ID
        /// </summary>
        public readonly string StartId = Guid.NewGuid().ToString();

        public ShellContext()
        {
            CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
        }
    }

    /// <summary>
    /// 命令通用接口
    /// </summary>
    public interface ICmd
    {
        /// <summary>
        /// 命令描述
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 命令名
        /// </summary>
        string Name { get; set; }

        
    }
}


namespace WS.Shell.Plugin
{
    using System.Threading.Tasks;

    /// <summary>
    /// 插件配置接口
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public interface IPluginConfig<TConfig>
    {
        Type ConfigType { get; }

        Task<PluginMessage> ConfigChanged(ShellContext context, TConfig newConfig);
        Task<TConfig> GetConfig(ShellContext context);
        TConfig GetDefaultConfig(ShellContext context);
        Task<bool> SaveConfig(TConfig cfg);
    }

    public class PluginMessage
    {
        public PluginMessage() { }

        public string Code { get; set; }
        public string Message { get; set; }

        public bool IsSuccess() { return false; }
    }
}
