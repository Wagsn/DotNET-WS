#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.CmdUnit
* 项目描述 ：
* 类 名 称 ：TestCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/23 10:41:12
* 更新时间 ：2018/11/23 10:41:12
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WS.IO;
using WS.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// 测试命令，用来测试编写的库函数是否正常工作
    /// </summary>
    public class TestCmd : CmdUnitBase
    {
        public TestCmd(ShellContext context) : base(context) {  }

        private readonly string testFileDir = "./test";

        /// <summary>
        /// string_regex: [".*"]
        /// arg_regex: [function_name_regex [".*"]*]
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            if (!string.IsNullOrWhiteSpace(arg))
            {
                // TODO 参数（Arguments）与选项（Option）拆分，在这里或者在ICmdUnit中
                string funcName = "";
                string funcArg = "";
                string[] funcArgs = new string[0];
                int ispace = arg.IndexOf(' ');
                if (ispace >= 0 && ispace<arg.Length)
                {
                    funcName = arg.Substring(0, ispace).Trim();
                    funcArg = arg.Substring(ispace).Trim();
                    Regex regex = new Regex("\".*\""); // 匹配字符串
                    Match match = regex.Match(funcArg);
                    // 提取参数
                    funcArgs = new string[match.Groups.Count];
                    for(int index = 0; index < match.Groups.Count; index++)
                    {
                        funcArgs[index] =match.Groups[index].Value;
                    }
                }
                else
                {
                    funcName = arg;
                }
                string[] args = arg.Trim().NormalSpace().Split(" ".ToCharArray());
                switch (funcName)
                {
                    case "-all":
                        Console.WriteLine("option -all");
                        break;
                    case "fileinfocreate":
                        File.WriteAllText(testFileDir + "/fileinfocreate.txt", "test fileinfo create", true);
                        break;
                    case "filewrite":
                        File.WriteAllText(testFileDir + "/filewrite.txt", "write text test");
                        break;
                    case "file":
                        string funArg = "";
                        if (args.Length > 1)
                        {
                            funArg = args[1];
                        }
                        break;
                    default:
                        Console.WriteLine($"Not found function for the name: <{funcName}>");
                        break;
                }
            }
            else
            {
                Console.WriteLine("test usage: test [function_name [function_arguments]]");
            }
            return -1;
        }

        public override void Init()
        {
            Name = "test";
            Desc = "测试命令，用来测试编写的库函数是否正常工作";
            Usage = "test [funcName [args]]";
        }
    }
}
