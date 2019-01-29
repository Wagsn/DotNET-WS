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
using WS.Text;

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
        /// <param name="source">源代码字符串</param>
        /// <returns>记号流</returns>
        public static List<Token> Lexing(string source)
        {
            return Lexing(Scanning(source));
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
            Regex IdentifierRegex = new Regex($"({IdentifierReg})");
            Regex NumericRegex = new Regex($"({NumericReg})");
            Regex PunctuatorRegex = new Regex($"({PunctuatorPattern})");
            foreach (var lexeme in predata)
            {
                if (IdentifierRegex.IsMatch(lexeme))
                {
                    tokens.Add(new Token
                    {
                        Kind = "Identifier",
                        Value = lexeme,
                        Loc = new Location
                        {
                            Start = new Position
                            {
                                line = currRow,
                                column = currCol
                            },
                            End = new Position
                            {
                                line = currRow,
                                column = currCol + lexeme.Length
                            },
                            Range = new Range
                            {
                                Start = currPos,
                                End = currPos + lexeme.Length
                            }
                        }
                    });
                    currPos += lexeme.Length;
                    currCol += lexeme.Length;
                }
                else if (NumericRegex.IsMatch(lexeme))
                {
                    tokens.Add(new Token
                    {
                        Kind = "Numeric",
                        Value = lexeme,
                        Loc = new Location
                        {
                            Start = new Position
                            {
                                line = currRow,
                                column = currCol
                            },
                            End = new Position
                            {
                                line = currRow,
                                column = currCol + lexeme.Length
                            },
                            Range = new Range
                            {
                                Start = currPos,
                                End = currPos + lexeme.Length
                            }
                        }
                    });
                    currPos += lexeme.Length;
                    currCol += lexeme.Length;
                }
                //符号或者空格
                else if (PunctuatorRegex.IsMatch(lexeme))
                {
                    // 换行
                    if (lexeme == "\r\n")
                    {
                        //tokens.Add(new Token
                        //{
                        //    Kind = "Punctuator",
                        //    Value = lexeme,
                        //    Loc = new Location
                        //    {
                        //        Start = new Position
                        //        {
                        //            line = currRow,
                        //            column = currCol
                        //        },
                        //        End = new Position
                        //        {
                        //            line = currRow,
                        //            column = currCol + lexeme.Length
                        //        },
                        //        Range = new Range
                        //        {
                        //            Start = currPos,
                        //            End = currPos + lexeme.Length
                        //        }
                        //    }
                        //});
                        currPos += lexeme.Length;
                        currRow++;
                        currCol = 0;
                        continue;
                    }
                    // 空格及制表符等不可见字符
                    if (string.IsNullOrWhiteSpace(lexeme))
                    {
                        currPos += lexeme.Length;
                        currCol += lexeme.Length;
                        continue;
                    }
                    // 符号
                    tokens.Add(new Token
                    {
                        Kind = "Punctuator",
                        Value = lexeme,
                        Loc = new Location
                        {
                            Start = new Position
                            {
                                line = currRow,
                                column = currCol
                            },
                            End = new Position
                            {
                                line = currRow,
                                column = currCol + lexeme.Length
                            },
                            Range = new Range
                            {
                                Start = currPos,
                                End = currPos + lexeme.Length
                            }
                        }
                    });
                    currPos += lexeme.Length;
                    currCol += lexeme.Length;
                }
            }
            Console.WriteLine($"Tokens:\r\n{JsonUtil.ToJson(tokens)}");
            return null;
        }

        /// <summary>
        /// 利用正则表达式生成预处理数据
        /// </summary>
        /// <param name="source">源代码</param>
        /// <returns></returns>
        public static string[] Scanning(string source)
        {
            var strs = new List<string>();
            // 扫描优先级（标识符>数值字面量>边界符）（暂时只做变量定义表达式（VariableDeclaration:"<id_name>[:<id_name>]:=<num_expr>;"）与数值四则运算表达式（BinaryExpression: "<id|num><[+|-|*|/]><id|num>"））
            Regex regex = new Regex($@"({IdentifierReg})|({NumericReg})|({PunctuatorPattern})");  // ({IdentifierRegex})|({NumericRegex})
            var matches = regex.Matches(source);
            string matchedstr = "";
            foreach (Match match in matches)
            {
                strs.Add(match.Value);
                matchedstr += match.Value;
            }
            Console.WriteLine($"Lexemes:\r\n{JsonUtil.ToJson(strs)}");
            if (source.Length != matchedstr.Length)
            {
                throw new Exception($"脚本解释器-词法分析器-预处理失败\r\nsource.Length: {source.Length}, matchedstr.Length: {matchedstr.Length}\r\nInput:\r\n{source}\r\nReverse:\r\n{matchedstr}");
            }
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
        private static readonly string PunctuatorPattern = @"(:=)|(=>)|(\r\n)|(\s+)|[\+\-\*/=\{\}\(\)\[\];:,\.]";

        ///// <summary>
        ///// 符号表达式（边界符与运算符）
        ///// </summary>
        //private static readonly Regex PunctuatorRegex = new Regex($"({PunctuatorPattern})");

        /// <summary>
        /// 数字表达式(非负浮点数)
        /// </summary>
        private static readonly string NumericReg = $@"(?<=\s|\b)(\d+)(\.\d+)?(?=\s|\b)";

        /// <summary>
        /// 标识表达式
        /// </summary>
        private static readonly string IdentifierReg = $@"(?<=\s|\b|_)({LetterRegex}|_)(_|{DigitRegex}|{LetterRegex})*(?=\s|\b)";
    }
}
