using System;
using System.Collections.Generic;

using WS.Core.Helpers;

namespace WS.Shell
{
    class Program
    {
        static int Main(string[] args)
        {
            // 对传入参数进行判断，没有参数就表示自我运行，载入配置之后再运行，没有配置就使用默认配置
            //int exitCode = App.New(args).Run();
            return App.New(args).Run();
        }
    }

    class App
    {
        /// <summary>
        /// 新建一个Shell
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static App New(string[] args)
        {
            return new App();
        }

        ///// <summary>
        ///// 程序启动时使用
        ///// </summary>
        //public static App Startup(ShellConfig config)
        //{
        //    //try
        //    //{
        //    //    ProcessStartInfo startInfo = new ProcessStartInfo();
        //    //    startInfo.FileName = "WindowsFormsApplication1.exe"; //启动的应用程序名称
        //    //    startInfo.Arguments = "我是由控制台程序传过来的参数，如果传多个参数用空格隔开" + " 第二个参数";
        //    //    startInfo.WindowStyle = ProcessWindowStyle.Normal;
        //    //    Process.Start(startInfo);

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw;
        //    //}
        //}

        /// <summary>
        /// 运行
        /// </summary>
        /// <returns></returns>
        public int Run()
        {
            // Wagsn Shell 的应用上下文初始化
            ShellContext AppContext = new ShellContext();
            // 打印版本信息
            Console.Write($"{AppContext.CopyrightInfo}\r\n\r\nWS {AppContext.CurrentDirectory}> ");

            // 一些中间临时变量
            string nextLine = null;
            string[] nextWords = null;
            // 命令
            string cmd = null;
            // 参数：可能是多个，交由处理函数自行切割
            string arg = null;

            // 一个简单的控制台循环
            while (true)
            {
                nextLine = Console.ReadLine().Trim();
                nextWords = nextLine.Split(" ");
                cmd = nextWords[0];
                arg = nextLine.Substring(cmd.Length).Trim();
                switch (cmd)
                {
                    case "":
                        break;
                    case "exit":
                        return 0;
                    case "clearlast":
                        Console.SetCursorPosition(0, Console.CursorSize - 1);
                        break;
                    case "test_split":
                        Console.WriteLine("String.Split \"hello world\" with \" \": " + JsonHelper.ToJson("hello world".Split(" "))); // ["hello","world"]
                        Console.WriteLine("String.Split \"\" with \" \": " + JsonHelper.ToJson("".Split(" ")));  // [""]
                        break;
                    default:
                        AppContext.cmdManager.Run(cmd, arg);
                        break;
                }
                Console.Write($"WS {AppContext.CurrentDirectory}> ");
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

        /// <summary>
        /// 命令集
        /// </summary>
        public readonly List<ICmdUnit> Cmds = new List<ICmdUnit>();

        /// <summary>
        /// 指令映射，是否用指令管理器来间接管理 CmdManager
        /// </summary>
        public readonly Dictionary<string, ICmdUnit> CmdMap = new Dictionary<string, ICmdUnit>();

        /// <summary>
        /// 
        /// </summary>
        public readonly CmdManager cmdManager = new CmdManager();
 
        /// <summary>
        /// 变量表(VariableTable) varName:varValue
        /// </summary>
        public readonly Dictionary<string, VarEntry> VarTable = new Dictionary<string, VarEntry>();

        // 类型表

        /// <summary>
        /// Shell Context Constructor
        /// </summary>
        public ShellContext()
        {
            // 运行时路径
            CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            // 指令集装载
            cmdManager.Add(new ToJsonCmd(this));
            cmdManager.Add(new HelpCmd(this));
            cmdManager.Add(new NowCmd());
            cmdManager.Add(new VarCmd(this));
            cmdManager.Add(new AddCmd(this));
            cmdManager.Add(new EchoCmd(this));
            cmdManager.Add(new IdCmd(this));
        }

        public class CmdManager
        {
            /// <summary>
            /// 指令映射
            /// </summary>
            public readonly Dictionary<string, ICmdUnit> CmdMap = new Dictionary<string, ICmdUnit>();

            ///// <summary>
            ///// Get("").Run("");
            ///// </summary>
            ///// <param name="key"></param>
            ///// <returns></returns>
            //public Runnable Get(string key)
            //{
            //    return CmdMap
            //}

            public void Add(ICmdUnit cmd)
            {
                // 验证
                CmdMap.Add(cmd.Name, cmd);
            }

            public void Remove(string key)
            {
                // 验证
                CmdMap.Remove(key);
            }

            public void Run(string line) { }

            public void Run(string cmd, string arg)
            {
                if (CmdMap.ContainsKey(cmd))
                {
                    CmdMap[cmd].Excute(arg);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("找不到关于 \"" + cmd + "\" 的命令\r\n");
                }
            }
        }
    }

    // 
    // 
    /// <summary>
    /// 包含变量的值部分，Variable Value
    /// str:string="hello"  // type = string value = "hello"
    /// num=163511  // type = var  value = 163511 
    /// </summary>
    public struct VarValue
    {
        public Type type;  // var type
        public object value; // var value
    }

    /// <summary>
    /// 包含一个变量的完整信息
    /// </summary>
    public struct VarEntry
    {
        public string raw;  // var raw -> str:string="hello" | var=163511
        public string name;  // var name
        public VarValue value; // var value
    }

    /// <summary>
    /// 函数通用接口(最小单位)
    /// </summary>
    public interface IFuncUnit
    {
        /// <summary>
        /// 函数描述(Description)
        /// </summary>
        string Desc { get; set; }

        /// <summary>
        /// 函数名：tosjon
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 函数使用方法描述(use-method)：tojson [argument] 格式参考[/doc/公约.md#命令调用方法]
        /// </summary>
        string Usage { get; set; }

        // 函数返回值与参数
    }
}
