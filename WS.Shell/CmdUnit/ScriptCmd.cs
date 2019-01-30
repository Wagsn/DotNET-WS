using System;
using System.Collections.Generic;
using System.Linq;
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
                            Name ="print",
                            
                        }
                    },
                    new VarEntry
                    {
                        // 对象名
                        Name = "import",
                        Raw = "import: String => Object { [native code] }",
                        // 对象值
                        Data = new PrintData()
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
                // load source
                readLine = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(readLine))
                {
                    continue;
                }
                // gen tokens
                var tokens = Lexer.Lexing(readLine);
                for (int i = 0; i < tokens.Count; i++)
                {
                    // 如何载入一条语句
                    // 然后分析语句
                    var token = tokens[i];
                    switch (token.Type)
                    {
                        case "Identifier":
                            // 如果 在当前上下文存在
                            if (context.VarTable.Any(v => v.Name == token.Value))
                            {
                                // 调用？
                                if(i+1<tokens.Count && tokens[i + 1].Value == "(")
                                {

                                }
                                //context.VarTable.Where(v => v.Name == token.Value).First().Data.Run();
                            }
                            // 不存在
                            else
                            {
                                // 声明？类型，变量
                            }

                            break;
                        case "Punctuator":

                            break;
                        case "String":

                            break;
                        default:
                            break;
                    }
                }
                // gen mainCall
                //var mainCall = new 
                // gen 
                // test:= import('./input/test.txt');
                // filestr = fs.read('./input/test.txt');
                // test2 := compiler.compile(filestr);
                switch (readLine.Trim().Trim(';'))
                {
                    case "testLexing()":
                        var filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input", "test.txt");
                        if (System.IO.File.Exists(filePath))
                        {
                            var fileStr = System.IO.File.ReadAllText(filePath);
                            Console.WriteLine(fileStr);
                            Console.WriteLine();
                            //Console.WriteLine(JsonUtil.ToJson(Lexer.Scanning(fileStr)));
                            //Lexer.Scanning(fileStr);
                            var tokens =Lexer.Lexing(fileStr);
                            string res = "";
                            tokens.Where(t => !(t.Type=="Comment"|| t.Type== "WhiteSpace")).ToList().ForEach(t => res += t?.Value);
                            Console.WriteLine($"No Comment|WhiteSpace Reverse:\r\n{res}");
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
}
