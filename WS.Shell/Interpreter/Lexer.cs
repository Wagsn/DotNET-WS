#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：Lexer
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 9:50:28
* 更新时间 ：2018/11/21 9:50:28
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WS.Shell
{
    /// <summary>
    /// 词法分析器(Lexical analyzer)
    /// </summary>
    public class Lexer
    {
        /// <summary>
        /// Lexing Lexical(词汇)
        /// </summary>
        /// <param name="sourceCode">源代码字符串</param>
        /// <returns>记号流</returns>
        public static List<Token> Lexing(string sourceCode)
        {
            return Lexing(Scanning(sourceCode));
        }

        /// <summary>
        /// 词法分析
        /// 通过预处理数据输出记号流
        /// </summary>
        /// <param name="predata">预处理数据</param>
        /// <returns></returns>
        public static List<Token> Lexing(string[] predata)
        {
            var tokens = new List<Token>();
            // 行列的改变和\n有关
            var currRow = 0;
            var currCol = 0;
            var currPos = 0;
            foreach(var lexeme in predata)
            {
                // 符号或者空格
                if (PunctuatorRegex.IsMatch(lexeme))
                {
                    tokens.Add(new Token
                    {
                        Type = "Punctuator",
                        Value = lexeme,
                        Loc = new Location
                        {
                            Start = new Position {},
                            End =  new Position {},
                            Range = new Range
                            {
                                Start = currPos,
                                End = currPos+lexeme.Length
                            }
                        }
                    });
                    currPos += lexeme.Length;
                }

            }
            return null;
        }

        /// <summary>
        /// 利用正则表达式生成预处理数据
        /// </summary>
        /// <param name="source">源代码</param>
        /// <returns></returns>
        public static string[] Scanning(string source)
        {
            //Console.WriteLine("输入字符串：" + source);
            var strs = new List<string>();
            // Person : Object { age : Number; getAge : Void => String { return caller.age; } ;  }; p: Person:= Person(){ age:= 152; }; print(p.getAge());
            // source.Length: 139, matchedstr.Length: 139
            // 扫描优先级（标识符>数值字面量>边界符）（暂时只做变量定义表达式（VariableDeclaration:"<id_name>[:<id_name>]:=<num_expr>;"）与数值四则运算表达式（BinaryExpression: "<id|num><[+|-|*|/]><id|num>"））
            Regex regex = new Regex($@"({IdentifierReg})|({NumericReg})|({PunctuatorPattern})");  // ({IdentifierRegex})|({NumericRegex})
            var matches = regex.Matches(source);
            //string matchedstr = "";
            foreach(Match match in matches)
            {
                strs.Add(match.Value);
                //matchedstr += match.Value;
            }
            //Console.WriteLine($"source.Length: {source.Length}, matchedstr.Length: {matchedstr.Length}\r\n{JsonUtil.ToJson(strs)}");
            return strs.ToArray();
        }

        /// <summary>
        /// 总表达式
        /// </summary>
        private static readonly string TotalRegex = $@"({IdentifierReg})|({NumericReg})";

        /// <summary>
        /// 英文字符表达式
        /// </summary>
        private static readonly string LetterRegex = @"[a-zA-Z]";

        /// <summary>
        /// 数字字符表达式
        /// </summary>
        private static readonly string DigitRegex = @"[0-9]";  // \d

        /// <summary>
        /// 符号表达式（边界符与运算符）
        /// </summary>
        private static readonly string PunctuatorPattern = @"(:=)|(=>)|[\+-\*/=\{\}\(\);:,\s\.]";

        /// <summary>
        /// 符号表达式（边界符与运算符）
        /// </summary>
        private static readonly Regex PunctuatorRegex = new Regex(PunctuatorPattern);

        /// <summary>
        /// 数字表达式
        /// </summary>
        private static readonly string NumericReg = $@"(?<=\s|\b){DigitRegex}+(?=\s|\b)";

        /// <summary>
        /// 标识表达式
        /// </summary>
        private static readonly string IdentifierReg = $@"(?<=\s|\b){LetterRegex}({DigitRegex}|{LetterRegex})*(?=\s|\b)";
        
        /// <summary>
        /// 预处理，将字符串拆分为最小表示单元
        /// (in: "var num: int =  46516"->out: ["var", " ", "num", ":", " ", "int", " ", "=", "  ", "46516"])
        /// (in: "var num/* note*/ :int;"->out: ["var", " ", "num", "/* note*/", " ", ":", "int", ";"])
        /// </summary>
        private string[] Pretreatment(string src)
        {
            return null;
        }
    }
}
