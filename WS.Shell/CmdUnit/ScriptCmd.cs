using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    public class ScriptCmd : CmdUnitBase
    {
        public RunContext RunContext { get; set; }

        public ScriptCmd(ShellContext context) : base(context) { }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            if (!string.IsNullOrWhiteSpace(arg))
            {
                var funName = "";
                //var funArg = "";
                switch (funName)
                {
                    case "exit()":
                    case "exit();":
                        return 0;
                    default:
                        Console.WriteLine($"输入：{arg}");
                        return 1;
                }
            }
            // 交互执行
            Console.WriteLine("Wagsn Script: 进入交互执行。。。\r\n");
            int code = 1;
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine().Trim().Trim(';');
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                switch (input)
                {
                    case "clear()":
                        Console.Clear();
                        break;
                    case "cursorleft()":
                        Console.WriteLine("> " + Console.CursorLeft);
                        break;
                    case "beep()":
                        // 通过控制台扬声器播放提示音
                        Console.Beep();
                        break;
                    case "exit()":
                        code = 0;
                        break;
                    default:
                        Console.WriteLine($"< {input}");
                        break;
                }
                if (code == 0)
                {
                    return 0;
                }
            }
            //RunContext
        }

        public override void Init()
        {
            Name = "script";
            Desc = "脚本";
            Usage = "script print(\"hello world\")";
        }
    }

    /// <summary>
    /// 脚本上下文
    /// </summary>
    public class ScriptContext
    {
        /// <summary>
        /// 变量表
        /// </summary>
        public IList<VarEntry> VarTable { get; set; }

    }
}
