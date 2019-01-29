using System;
using System.Collections.Generic;
using System.Text;
using WS.Text;

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
            // 创建上下文
            ScriptContext context = new ScriptContext
            {
                VarTable = new List<VarEntry>
                {
                    new VarEntry
                    {
                        Name = "print",
                        Raw = "print: String => Void{ [native code] }",
                        Data = new VarData
                        {

                        }
                    },
                    new VarEntry
                    {
                        // 对象名
                        Name = "import",
                        Raw = "import: String => Object { [native code] }",
                        // 对象值
                        Data = new VarData
                        {
                            
                        }
                    }
                }
            };
            // 交互执行
            Console.WriteLine("Wagsn Script: 进入交互执行。。。\r\n");
            int code = 1;
            string readLine =null;
            while (true)
            {
                Console.Write("> ");
                readLine = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(readLine))
                {
                    continue;
                }
                Lexer.Lexing(readLine);
                // test:= import('./input/test.txt');
                // filestr = fs.read('./input/test.txt');
                // test2 := compiler.compile(filestr);
                // tokens 
                // print('161615');
                // Person : Object { age : Number; getAge : Void => String { return caller.age; } ;  }; p: Person:= Person(){ age:= 152; }; print(p.getAge());
                //Lexer.Scanning(readLine);
                //Console.WriteLine();
                switch (readLine.Trim().Trim(';'))
                {
                    case "testScanning()":
                        var filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input", "test.txt");
                        if (System.IO.File.Exists(filePath))
                        {
                            var fileStr = System.IO.File.ReadAllText(filePath);
                            Console.WriteLine(fileStr);
                            Console.WriteLine();
                            //Console.WriteLine(JsonUtil.ToJson(Lexer.Scanning(fileStr)));
                            //Lexer.Scanning(fileStr);
                            Lexer.Lexing(fileStr);
                        }
                        else
                        {
                            Console.WriteLine("文件不存在：" + filePath);
                        }
                        break;
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
                        Console.WriteLine($"< {readLine}");
                        break;
                }
                if (code == 0) return 0;
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

        public ScriptContext()
        {
            VarTable = new List<VarEntry>();
        }
    }
}
