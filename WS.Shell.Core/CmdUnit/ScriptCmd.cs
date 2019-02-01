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
            // 解析参数
            if (!string.IsNullOrWhiteSpace(arg))
            {
                var funName = arg.Trim(' ').Trim(';');
                switch (funName)
                {
                    case "exit()":
                        Console.WriteLine("离开交互执行");
                        return 0;
                    default:
                        Console.WriteLine($"输入参数：{arg.Trim(' ')}");
                        return 1;
                }
            }
            // 创建价平户执行上下文
            ScriptContext context = new ScriptContext
            {
                VarTable = new List<VarEntry>
                {
                    new VarEntry
                    {
                        Name = "print",
                        Raw = "print: String => Void{ [native code] }",
                        Data = new PrintData()
                    },
                    new VarEntry
                    {
                        // 对象名
                        Name = "import",
                        Raw = "import: String => Object { [native code] }",
                        // 对象值
                        Data = new PrintData()
                    },
                    new VarEntry
                    {
                        Name ="load",
                        Raw = "load: String=>String { [native code] }",
                        Data = new LoadData()
                    }
                }
            };
            // 交互执行
            Console.WriteLine("Wagsn Script: 进入交互执行。。。\r\n");
            // 退出码
            int exitCode = -1;
            // 读入代码
            string readLine =null;
            // 进入交互循环
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
                var tokens = Lexer.Lexing(readLine).Where(t => !(t.Type == "Comment" || t.Type == "WhiteSpace")).ToList();
                // 语句开始 遇到SEM变为true
                bool stmtStartFlag = true;
                // statement 语句拆分
                List<List<Token>> stmts = new List<List<Token>>();
                // 当前语句
                List<Token> currStmt = new List<Token>();
                VarData last = new NoneData();
                // 位置暂存栈
                Stack<int> stashStack = new Stack<int>();
                //stashStack.Peek();
                // 当前位置
                int currPos = -1;
                // 根节点

                for (int i = 0; i < tokens.Count; i++)
                {

                    // 得到语句 方式一：用一个数组来保存（不支持嵌套）
                    //switch (tokens[i].Type)
                    //{

                    //}
                    // 如何载入一条语句 暂时只有单条语句(赋值语句 num:= 123.456;, 函数调用语句 print('wagssn');)
                    // 单函数无参数调用语句

                    // 单函数单参数调用语句（<ID> <LP> <LIT> <RP> <SEM> ） 必须要有5个Token, （函数调用语句最少4个Token）
                    if (tokens.Count - i >= 5 && tokens[i].Type == "Identifier" && tokens[i + 1].Value == "(" && IsLiteral(tokens[i + 2].Type) && tokens[i + 3].Value == ")" && tokens[i + 4].Value == ";")
                    {
                        // 获取函数
                        if(tokens[i].Value == "print" && context.VarTable.Any(v => v.Name == "print"))
                        {
                            var print = context.VarTable.Where(v => v.Name == "print").First();
                            // 参数构造
                            List<VarData> args = new List<VarData>();
                            if(tokens[i + 2].Type == "String")
                            {
                                args.Add(new StringData(tokens[i + 2].Value));
                            }
                            else //if(tokens[i + 2].Type == "Numeric")
                            {
                                args.Add(new LiteralData
                                {
                                    // 需要解析Token
                                    Data = tokens[i + 2].Value,
                                    Kind = "Object",
                                    Raw = tokens[i + 2].Value
                                });
                            }
                            // 函数调用 这里没有注入调用上下文（创建一个参数类型，把调用上下文封装到参数对象中去）
                            var result = print.Data.Run(args.ToArray());
                            last = result;
                        }
                        else if (tokens[i].Value == "load" && context.VarTable.Any(v => v.Name == "load"))
                        {
                            var load = context.VarTable.Where(v => v.Name == "load").First();
                            // 参数构造
                            List<VarData> args = new List<VarData>();
                            if (tokens[i + 2].Type == "String")
                            {
                                args.Add(new StringData(tokens[i + 2].Value));
                            }
                            else //if(tokens[i + 2].Type == "Numeric")
                            {
                                args.Add(new LiteralData
                                {
                                    // 需要解析Token
                                    Data = tokens[i + 2].Value,
                                    Kind = "Object",
                                    Raw = tokens[i + 2].Value
                                });
                            }
                            // 函数调用 这里没有注入调用上下文（创建一个参数类型，把调用上下文封装到参数对象中去）
                            var result = load.Data.Run(args.ToArray());
                            last = result;
                        }
                        stmts.Add(tokens.Skip(i).Take(5).ToList());
                        i += 4;
                    }
                }
                Console.WriteLine($"Statements:\r\n{JsonUtil.ToJson(stmts)}");
                // gen mainCall
                //var mainCall = new 
                // gen 
                // test:= import('./input/test.txt');
                // filestr = fs.read('./input/test.txt');
                // test2 := compiler.compile(filestr);
                switch (readLine.Trim().Trim(';'))
                {
                    case "clear();":
                    case "clear()":
                        Console.Clear();
                        break;
                    case "cursorleft();":
                    case "cursorleft()":
                        Console.WriteLine("> " + Console.CursorLeft);
                        break;
                    case "beep();":
                    case "beep()":
                        // 通过控制台扬声器播放提示音
                        Console.Beep();
                        break;
                    case "exit();":
                    case "exit()":
                        exitCode = 0;
                        break;
                    default:
                        break;
                }
                foreach(var stmt in stmts)
                {
                    var s = "";
                    stmt.ForEach(t => s += (t.Kind +" "));
                    Console.WriteLine(s);
                }
                // 打印返回值
                Console.WriteLine($"< {last.Data}");
                if (exitCode >= 0) return 0;
            }
            //RunContext
        }

        public override void Init()
        {
            Name = "script";
            Desc = "脚本";
            Usage = "script print(\"hello world\")";
        }

        /// <summary>
        /// 下一条语句
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static dynamic NextStatement(Stack<Statement> stmtStack, List<Token> tokens)
        {
            var currPos = 0;
            List<Statement> Body = new List<Statement>();
            var exprStack = new Stack<Expression>();
            NextExpression(exprStack, tokens.Skip(currPos+1).Take(tokens.Count).ToList());
            var currStmt = new ExpressionStatement();

            return null;
        }

        /// <summary>
        /// 下一条表达式
        /// </summary>
        /// <param name="exprStack"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static dynamic NextExpression(Stack<Expression> exprStack, List<Token> tokens)
        {
            var currPos = 0;
            var currToken = tokens[currPos];
            switch (currToken.Type)
            {
                case "Identifier":
                    var idExpr = new IdentiferExpression();
                    idExpr.StartPos = exprStack.Peek().EndPos;
                    idExpr.EndPos = idExpr.StartPos + 1;
                    exprStack.Push(idExpr);
                    break;
                case "Numeric":
                    var numExpr = new NumericExpression();
                    numExpr.StartPos = exprStack.Peek().EndPos;
                    numExpr.EndPos = numExpr.StartPos + 1;
                    exprStack.Push(numExpr);
                    break;
                case "String":
                    var strExpr = new StringExpression();
                    strExpr.StartPos = exprStack.Peek().EndPos;
                    strExpr.EndPos = strExpr.StartPos + 1;
                    exprStack.Push(strExpr);
                    break;
                case "Punctuator":
                    switch (tokens[currPos].Value)
                    {
                        // 成员操作符
                        case ".":
                            var last = exprStack.Pop();
                            var memExpr = new MemberExpression();
                            NextExpression(exprStack, tokens.Skip(currPos + 1).Take(tokens.Count).ToList());  // 这里会push一个表达式
                            memExpr.Object = last;
                            memExpr.Property = exprStack.Pop();
                            memExpr.StartPos = memExpr.Object.StartPos;
                            memExpr.EndPos = memExpr.Property.EndPos;
                            exprStack.Push(memExpr);
                            break;
                    }
                    break;

            }
            return null;
        }

        /// <summary>
        /// 是否是字面量
        /// </summary>
        /// <param name="type">Token类型</param>
        /// <returns></returns>
        public static bool IsLiteral(string type)
        {
            return type == "String" || type == "Numeric" || type == "Boolean" || type == "Number";
        }

        /// <summary>
        /// 字面量 Literal
        /// </summary>
        public static readonly string LitPattern = $@"({Lexer.StringPattern})|({Lexer.NumericPattern})";

        /// <summary>
        /// 表达式
        /// </summary>
        public static readonly string ExprPattern = $@"({LitPattern})|({Lexer.IdPattern})|({Lexer.IdPattern}\(({Lexer.IdPattern}(\.{Lexer.IdPattern})*)?\))";

        /// <summary>
        /// 语句
        /// </summary>
        public static readonly string StmtPattern = $@"({ExprPattern})?;"; 
    }

    public class ExpressionStatement : Statement { }

    public class Statement
    {
        public int StartPos { get; set; }

        public int EndPos { get; set; }
    }

    public class MemberExpression : Expression
    {
        public Expression Object { get; set; }

        public Expression Property { get; set; }
    }

    public class StringExpression : Expression { }

    public class NumericExpression : Expression { }

    public class IdentiferExpression: Expression { }

    public class Expression
    {
        public int StartPos { get; set; }
        
        public int EndPos { get; set; }
    }
}
