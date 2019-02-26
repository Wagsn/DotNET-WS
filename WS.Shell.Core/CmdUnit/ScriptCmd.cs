using System;
using System.Collections.Generic;
using System.Dynamic;
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
            Usage = "script";
        }


        public static void ReadStatements(List<Token> tokens)
        {
            // 将tokens转换成语句列表
            int currPos = 0;
        }

        /// <summary>
        /// 下一条语句
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Statement NextStatement(Stack<Statement> stmtStack, List<Token> tokens)
        {
            var currPos = 0;
            List<Statement> Body = new List<Statement>();
            var exprStack = new Stack<Expression>();
            NextExpression(exprStack, tokens.Skip(currPos+1).Take(tokens.Count).ToList());
            var last = exprStack.Pop();
            
            if (tokens[last.EndPos+1].Kind == "SEM")
            {
                var currStmt = new ExpressionStatement
                {
                    StartPos = last.StartPos,
                    EndPos = last.EndPos + 1,
                    Expression = last
                };
                return currStmt;
            }
            else
            {
                Console.WriteLine("语法错误，表达式不以分号结尾。");
                return null;
            }
        }

        /// <summary>
        /// 下一条表达式
        /// </summary>
        /// <param name="exprStack"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static dynamic NextExpression(Stack<Expression> exprStack, List<Token> tokens, int startPos = 0)
        {
            var currPos = startPos;
            var currToken = tokens[currPos];
            switch (currToken.Type)
            {
                case "Identifier":
                    var idExpr = new IdentiferExpression
                    {
                        StartPos = exprStack.Peek().EndPos
                    };
                    idExpr.EndPos = idExpr.StartPos + 1;
                    exprStack.Push(idExpr);
                    break;
                case "Numeric":
                    var numExpr = new NumericExpression
                    {
                        StartPos = exprStack.Peek().EndPos
                    };
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

    /// <summary>
    /// 语句块
    /// </summary>
    public class BlockStatement : Statement
    {
        public List<Statement> Statements { get; set; }
    }

    /// <summary>
    /// 表达式语句
    /// </summary>
    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; set; }
    }

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

    /// <summary>
    /// 左结合 
    /// 主要是解析的时候
    /// 
    /// </summary>
    public class BinaryExpression : Expression
    {
        /// <summary>
        /// 操作符
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 左
        /// </summary>
        public Exception Left { get; set; }
        /// <summary>
        /// 右
        /// </summary>
        public Exception Right { get; set; }
    }

    /// <summary>
    /// 赋值表达式 右结合
    /// </summary>
    public class AssignmentExpression : Expression
    {
        /// <summary>
        /// 操作符
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 左
        /// </summary>
        public Exception Left { get; set; }
        /// <summary>
        /// 右
        /// </summary>
        public Exception Right { get; set; }
    }

    /// <summary>
    /// 逻辑(布尔)表达式 左结合
    /// </summary>
    public class LogicalExpression : Expression
    {
        /// <summary>
        /// 操作符
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 左
        /// </summary>
        public Exception Left { get; set; }
        /// <summary>
        /// 右
        /// </summary>
        public Exception Right { get; set; }
    }

    /// <summary>
    /// 标识符
    /// </summary>
    public class IdentiferExpression: Expression
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// 调用表达式
    /// </summary>
    public class CallExpression
    {
        /// <summary>
        /// 调用者
        /// </summary>
        public Exception Callee { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<Exception> Arguments { get; set; }
    }

    public class Expression
    {
        public VarData Data { get; set; }

        public int StartPos { get; set; }
        
        public int EndPos { get; set; }
    }

    /**
     * code is far away from bug *with the animal protecting 
    *  ┏┓　　　┏┓
    *┏┛┻━━━┛┻┓
    *┃　　　　　　　┃ 　
    *┃　　　━　　　┃
    *┃　┳┛　┗┳　┃
    *┃　　　　　　　┃
    *┃　　　┻　　　┃
    *┃　　　　　　　┃
    *┗━┓　　　┏━┛
    *　　┃　　　┃神兽保佑
    *　　┃　　　┃代码无BUG！
    *　　┃　　　┗━━━┓
    *　　┃　　　　　　　┣┓
    *　　┃　　　　　　　┏┛
    *　　┗┓┓┏━┳┓┏┛
    *　　　┃┫┫　┃┫┫
    *　　　┗┻┛　┗┻┛ 
    */

    /**
     * E->T|EAT  // Expression
     * T->F|TMF  // Term
     * F->(E)|i  // Factor
     * A->+|-  // Add
     * M->*|/  // Mult
     * 
     */
    /**
     * Expression -> Expression Operator Expression
     * Statement -> Expression Semicolon 
     * Expression -> CallExpression | MemberExpression | LogicalExpression | AssignmentExpression
     * AssignmentExpression -> MemberExpression "=" Expression | Identifier "=" Expression 
     * LogicalExpression -> Expression | LogicalExpression "&&" Expression | LogicalExpression "||" Expression   // "&&" > "||"
     * MemberExpression -> Expression "." Identifier | Expression "[" Expression "]"
     * CallExpression -> Expression "(" Arguments ")"     // { Callee, Operator, Arguments }
     * Arguments -> WhiteSpace | Expression | Expression "," Arguments   // 不结合
     * Semicolon -> ";"
     * Literal -> String | Number | Boolean | "None"
     * Boolean -> "True" | "False"
     * String -> 
     * Number -> Int Frac Exp
     * Int -> Digit | OneNine Digit | "-" Digit | "-" OneNine Digit
     * Digits -> Digit | Digit Digits
     * Digit -> "0" | OneNine
     * OneNine -> "0" . "9"
     * Frac -> "" | "." Digits
     * Exp -> "" |  "E" Sign Digits | "e" Sign Digits
     * Sign -> "" | "+"" | "-"
     */
}


namespace WS.Shell.Core
{
    public class WSData
    {

    }

    /// <summary>
    /// 值
    /// </summary>
    public class WSValue : WSData
    {
        /// <summary>
        /// 转型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WSValue Cast<T>(T value)
        {
            return null;
        }

        public WSBoolean ToBoolean()
        {
            return WSBoolean.New(false);
        }

        public WSObject ToObject()
        {
            return WSObject.New();
        }

        public WSNumber ToNumber()
        {
            return WSNumber.New(0);
        }

        public new WSString ToString()
        {
            return WSString.New("");
        }

        public bool DeepEquals(WSValue that)
        {
            return false;
        }

        public bool BooleanValue()
        {
            return false;
        }

    }

    /// <summary>
    /// 字面量
    /// </summary>
    public class WSLiteral : WSValue
    {
    }

    /// <summary>
    /// 布尔量
    /// </summary>
    public class WSBoolean : WSLiteral
    {
        private bool V { get; set; }

        public static WSBoolean New(bool value)
        {
            return new WSBoolean
            {
                V = false
            };
        }

        public bool Value()
        {
            return false;
        }
    }

    public class WSNumber : WSLiteral
    {
        public double V { get; set; }

        public static WSNumber New(double value)
        {
            return new WSNumber
            {
                V = value
            };
        }

        public static WSNumber Cast(WSValue obj)
        {
            return new WSNumber
            {
                V =0
            };
        }

        public double Value()
        {
            return V;
        }
    }

    public class WSInteger : WSNumber
    {
        public static WSInteger New(int value)
        {
            return new WSInteger
            {
                V = value
            };
        }

        public new static WSInteger Cast(WSValue obj)
        {
            return new WSInteger
            {
                V = 0
            };
        }

        public new int Value()
        {
            return (int)V;
        }
    }

    public class WSName : WSLiteral
    {
        public static WSName Cast(WSValue obj)
        {
            return new WSName();
        }
    }

    public class WSString : WSName
    {
        private static readonly string empty = "";

        public static WSString Empty()
        {
            return new WSString
            {
                V = ""
            };
        }

        public static WSString New (string value)
        {
            return new WSString
            {
                V = value
            };
        }

        public new static WSString Cast(WSValue value)

        {
            return new WSString
            {
                V = empty
            };
        }

        public static WSString Concat(WSString left, WSString right)
        {
            return new WSString
            {
                V = left.V + right.V
            };
        }

        private string V { get; set; }

        public string Value()
        {
            return V;
        }

        public int Length()
        {
            return V.Length;
        }
    }

    public class WSObject : WSValue
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static WSObject New()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 没有句柄，不计数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(WSValue key, WSValue value)
        {
            return false;
        }

        public WSValue Get(WSValue value)
        {
            return null;
        }

        public bool Has(WSValue key)
        {
            return false;
        }

        public WSObject Clone()
        {
            return null;
        }

        public bool Delete(WSValue key)
        {
            return false;
        }

        public bool CallAsFunction(WSValue recv, List<WSValue> argv)
        {
            return false;
        }
    }

    public class StringObject : WSObject
    {
        public WSString ValueOf()
        {
            return WSString.New("");
        }

        public static WSValue New(string value)
        {
            return null;
        }
    }

    public class WSArray: WSObject
    {
        public static WSArray New()
        {
            return new WSArray();
        }

        public int Length()
        {
            return 0;
        }
    }

    /// <summary>
    /// 函数实例
    /// </summary>
    public class WSFunction : WSObject
    {
        public static WSFunction New(Func<List<WSValue>, WSValue> fun)
        {
            return new WSFunction
            {
                Fun =fun
            };
        }
        
        private Func<List<WSValue>, WSValue> Fun { get; set; }

        private string Name { get; set; } = "";

        public WSObject NewInstance()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 函数调用
        /// </summary>
        /// <param name="recv">调用者</param>
        /// <param name=""></param>
        /// <returns></returns>
        public WSValue Call(WSValue recv, List<WSValue> argv)
        {
            return Fun(argv);
        }

        public void SetName(WSString name)
        {
            Name = name.Value();
        }

        public WSValue GetName()
        {
            return WSString.New(Name);
        }
    }

    /// <summary>
    /// 模板（函数模板，对象模板）
    /// The superclass of object and function templates.
    /// </summary>
    public class WSTemplate : WSData
    {
        public void Set(WSName name, WSData value)
        {
        }
        public void Set(string name, WSData value)
        {
        }
    }

    public class WSObjectTemplate : WSTemplate
    {
        public static WSObjectTemplate New()
        {
            return new WSObjectTemplate
            {

            };
        }
    }

    /// <summary>
    /// 函数模板
    /// </summary>
    public class WSFunctionTemplate : WSTemplate
    {
        public static WSFunctionTemplate New()
        {
            return new WSFunctionTemplate
            {
            };
        }

        public static WSFunctionTemplate New(Func<List<WSValue>, WSValue> func)
        {
            return new WSFunctionTemplate
            {

            };
        }

        public static void Test()
        {
            WSFunctionTemplate t = WSFunctionTemplate.New();
            t.Set("func_property", WSNumber.New(1));

            WSTemplate proto_t = t.PrototypeTemplate();
            proto_t.Set("proto_method", WSFunctionTemplate.New());
            proto_t.Set("proto_const", WSNumber.New(2));

            WSObjectTemplate instance_t = t.InstanceTemplate();
            instance_t.Set("instance_property", WSNumber.New(3));

            WSFunction function = t.GetFunction();
            WSObject instance = function.NewInstance();

            WSFunctionTemplate parent = t;
            WSFunctionTemplate child = WSFunctionTemplate.New();
            child.Inherit(parent);

            WSFunction child_function = child.GetFunction();
            WSObject child_instance = child_function.NewInstance();
        }

        public void Inherit(WSFunctionTemplate parent)
        {
            throw new NotImplementedException();
        }

        public WSFunction GetFunction()
        {
            return new WSFunction();
        }
        
        /// <summary>
        /// 原型模板
        /// </summary>
        /// <returns></returns>
        public WSObjectTemplate PrototypeTemplate()
        {
            return WSObjectTemplate.New();
        }

        public WSObjectTemplate InstanceTemplate()
        {
            return WSObjectTemplate.New();
        }
    }

    /// <summary>
    /// The information passed to a property callback about the context of the property access.
    /// </summary>
    public class PropertyCallbackInfo
    {
        /// <summary>
        /// 值
        /// </summary>
        /// <returns></returns>
        public WSValue Data()
        {
            return WSString.New("");
        }

        /// <summary>
        /// 调用者
        /// </summary>
        /// <returns></returns>
        public WSObject This()
        {
            return WSObject.New();
        }

        /// <summary>
        /// 上下文
        /// </summary>
        /// <returns></returns>
        public WSObject Holder()
        {
            return WSObject.New();
        }
        
        /// <summary>
        /// 返回值
        /// </summary>
        /// <returns></returns>
        public WSObject GetReturnValue()
        {
            return WSObject.New();
        }
    }

    public class Context
    {

    }
}


namespace WS.Script.Core
{
    /// <summary>
    /// 值对象和模板类型的超类
    /// The superclass of values and API object templates.
    /// </summary>
    public class WSData
    {

    }

    /// <summary>
    /// 模板，类型信息描述
    /// </summary>
    public class WSTemplate : WSData
    {

    }

    /// <summary>
    /// 值，对象实例描述
    /// The superclass of all JavaScript values and objects.
    /// </summary>
    public class WSValue : WSData
    {

    }

    /// <summary>
    /// 字面量实例描述
    /// </summary>
    public class WSLiteral : WSValue
    {
    }

    public class WSNumber : WSLiteral
    {
    }
    public class WSString : WSLiteral
    {
    }
    public class WSBoolean : WSLiteral
    {
    }
    

    /// <summary>
    /// 实例对象描述（动态对象）
    /// </summary>
    public class WSObject : WSValue
    {
        public bool Has(WSValue key)
        {
            return false;
        }

        public bool Set(WSValue key, WSValue value)
        {
            return false;
        }

        public WSValue Get(WSValue key)
        {
            return null;
        }

        public bool Delete(WSValue key)
        {
            return false;
        }

        /// <summary>
        /// 将当前对象作为函数执行
        /// </summary>
        /// <param name="recv">调用者</param>
        /// <param name="argv">参数</param>
        /// <returns></returns>
        public WSValue CallAsFunction(WSValue recv, List<WSValue> argv)
        {
            return null;
        }

        public WSValue CallAsConstructor(List<WSValue> argv)
        {
            return null;
        }

        public WSValue Run(WSValue recv, List<WSValue> argv)
        {
            return null;
        }
    }

    /// <summary>
    /// 数组
    /// </summary>
    public class WSArray : WSObject
    {
    }

    /// <summary>
    /// 函数对象描述
    /// </summary>
    public class WSFunction : WSObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recv"></param>
        /// <param name="argv"></param>
        /// <returns></returns>
        public WSValue Call(WSValue recv, List<WSValue> argv)
        {
            return null;
        }

        public void SetName(WSString name)
        {

        }

        public WSValue GetName()
        {
            return null;
        }
    }

    /// <summary>
    /// 日期时间
    /// </summary>
    public class WSDate : WSObject
    {
    }

    public class WSObjectTemplate:WSData
    {
        public WSObject NewInstance()
        {
            return null;
        }
    }
}