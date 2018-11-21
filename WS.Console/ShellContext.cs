#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：ShellContext
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 11:18:00
* 更新时间 ：2018/11/21 11:18:00
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
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
        /// 应用名称
        /// </summary>
        public static string AppName = "Wagsn Shell";

        /// <summary>
        /// 应用ID
        /// </summary>
        public static readonly string AppId = "d3eef080-f523-404c-8b0c-d7c764f59190";

        /// <summary>
        /// 应用版本
        /// </summary>
        public static readonly string AppVersion = "1.0.0";

        /// <summary>
        /// 版权信息
        /// </summary>
        public static string CopyrightInfo = "版权所有 (C) Wagsn。保留所有权利。";

        /// <summary>
        /// 欢迎信息
        /// </summary>
        public string HelloInfo = AppName + "\r\n" + CopyrightInfo;

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

            /// <summary>
            /// 指令数量
            /// </summary>
            public int Count
            {
                get
                {
                    return CmdMap.Count;
                }
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
}
