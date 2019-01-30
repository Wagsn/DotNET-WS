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
        /// 评估器（Evaluator）
        /// 评估器有时会抑制语素，被抑制的语素（例如空白语素和注释语素）随后不会被送入语法分析器。
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
            Regex IdentifierRegex = new Regex($"({IdentifierPattern})");
            Regex NumericRegex = new Regex($"({NumericPattern})");
            Regex PunctuatorRegex = new Regex($"({PunctuatorPattern})");
            Regex StringRegex = new Regex($"({StringPattern})");
            Regex CommentRegex = new Regex($"({CommentPattern})");
            Regex WhiteSpaceRegex = new Regex($"({WhiteSpacePattern})");
            foreach (var lexeme in predata)
            {
                // 注释 注释语素
                if (CommentRegex.IsMatch(lexeme))
                {
                    var token = new Token
                    {
                        Type = "Comment",
                        Value = lexeme
                    };
                    token.Loc = new Location
                    {
                        Start = new Position
                        {
                            Line = currRow,
                            Column = currCol
                        },
                        Range = new Range
                        {
                            Start = currPos,
                            End = currPos + lexeme.Length
                        }
                    };
                    // 计算换行数
                    var nlc = Regex.Matches(lexeme, "\n|$").Count;
                    // 没有换行
                    if (nlc == 0)
                    {
                        token.Loc.End = new Position
                        {
                            Line = currRow,
                            Column = currCol + lexeme.Length
                        };
                        currCol += lexeme.Length;
                    }
                    // 有换行
                    else
                    {
                        // 计算末尾行列号
                        currCol = lexeme.Length - (lexeme.LastIndexOf('\n') + 1);
                        currRow += nlc;
                        token.Loc.End = new Position
                        {
                            Line = currRow,
                            Column = currCol
                        };
                    }
                    currPos += lexeme.Length;
                    tokens.Add(token);
                }
                else
                // 空格 空白语素
                if (WhiteSpaceRegex.IsMatch(lexeme))
                {
                    var token = new Token
                    {
                        Type = "WhiteSpace",
                        Value = lexeme,
                        Loc = new Location
                        {
                            Start = new Position
                            {
                                Line =currRow,
                                Column =currCol,
                            },
                            Range = new Range
                            {
                                Start = currPos,
                                End = currPos+lexeme.Length
                            }
                        }
                    };
                    // 计算换行数
                    var nlc = Regex.Matches(lexeme, "\n|$").Count;
                    // 没有换行
                    if (nlc == 0)
                    {
                        token.Loc.End = new Position
                        {
                            Line = currRow,
                            Column = currCol + lexeme.Length
                        };
                        currCol += lexeme.Length;
                    }
                    // 有换行
                    else
                    {
                        // 计算末尾行列号
                        currCol = lexeme.Length - (lexeme.LastIndexOf('\n') + 1);
                        currRow += nlc;
                        token.Loc.End = new Position
                        {
                            Line = currRow,
                            Column = currCol
                        };
                    }
                    currPos += lexeme.Length;
                    tokens.Add(token);

                }
                else
                // 字符串字面量
                if (StringRegex.IsMatch(lexeme))
                {
                    tokens.Add(new Token
                    {
                        // 字符串字面量
                        Type = "String",
                        Value = lexeme,
                        Loc = new Location
                        {
                            Start = new Position
                            {
                                Line = currRow,
                                Column = currCol
                            },
                            End = new Position
                            {
                                Line = currRow,
                                Column = currCol + lexeme.Length
                            },
                            Range = new Range
                            {
                                Start = currPos,
                                End = currPos + lexeme.Length
                            }
                        }
                    });
                    currCol += lexeme.Length;
                    currPos += lexeme.Length;
                }
                // 标识符
                else if (IdentifierRegex.IsMatch(lexeme))
                {
                    tokens.Add(new Token
                    {
                        // 标识符
                        Type = "Identifier",
                        Value = lexeme,
                        Kind = "ID",
                        Loc = new Location
                        {
                            Start = new Position
                            {
                                Line = currRow,
                                Column = currCol
                            },
                            End = new Position
                            {
                                Line = currRow,
                                Column = currCol + lexeme.Length
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
                // 数值字面量
                else if (NumericRegex.IsMatch(lexeme))
                {
                    tokens.Add(new Token
                    {
                        // 数值字面量
                        Type = "Numeric",
                        Value = lexeme,
                        Loc = new Location
                        {
                            Start = new Position
                            {
                                Line = currRow,
                                Column = currCol
                            },
                            End = new Position
                            {
                                Line = currRow,
                                Column = currCol + lexeme.Length
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
                // 符号
                else if (PunctuatorRegex.IsMatch(lexeme))
                {
                    var token = new Token
                    {
                        // 符号
                        Type = "Punctuator",
                        Value = lexeme,
                        Loc = new Location
                        {
                            Start = new Position
                            {
                                Line = currRow,
                                Column = currCol
                            },
                            End = new Position
                            {
                                Line = currRow,
                                Column = currCol + lexeme.Length
                            },
                            Range = new Range
                            {
                                Start = currPos,
                                End = currPos + lexeme.Length
                            }
                        }
                    };
                    switch (lexeme)
                    {
                        case "(":
                            token.Kind = "LP";
                            break;
                        case ")":
                            token.Kind = "RP";
                            break;
                        case ";":  // semicolon
                            token.Kind = "SEM";
                            break;
                        default:
                            token.Kind = "UNK"; // Unknown
                            break;
                    }
                    // 符号
                    tokens.Add(token);
                    currPos += lexeme.Length;
                    currCol += lexeme.Length;
                }
            }
            Console.WriteLine($"Tokens:\r\n{JsonUtil.ToJson(tokens)}");
            Console.WriteLine($"Tokens:\r\n{JsonUtil.ToJson(tokens.Select(t => $"({t.Type}, {t.Value})"))}");
            return tokens;
        }

        /// <summary>
        /// 利用正则表达式生成预处理数据
        /// Tokenization
        /// </summary>
        /// <param name="source">源代码</param>
        /// <returns></returns>
        public static string[] Scanning(string source)
        {
            var strs = new List<string>();
            // 扫描优先级（注释>字符串字面量>标识符>数值字面量>边界符）（暂时只做变量定义表达式（VariableDeclaration:"<id_name>[:<id_name>]:=<num_expr>;"）与数值四则运算表达式（BinaryExpression: "<id|num><[+|-|*|/]><id|num>"））
            Regex regex = new Regex(TotalRegex);  // ({CommentPattern})|({StringPattern})|({IdentifierPattern})|({NumericPattern})|({PunctuatorPattern})
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
        /// 总表达式 字符串字面量-注释-空格-数值字面量-符号-标识符
        /// </summary>
        private static readonly string TotalRegex = @"('.*?')|(/\*(.|\n)*?\*/)|(//.*[\n$])|(\s+)|((?=\s|\b)(\d+)(\.\d+)?(?=\s|\b))|((:=)|(=>)|[<>\+\-\*/=\{\}\(\)\[\];:,\.])|([a-zA-Z][a-zA-Z0-9]*)";

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
        private static readonly string PunctuatorPattern = @"(:=)|(=>)|[<>\+\-\*/=\{\}\(\)\[\];:,\.]";

        ///// <summary>
        ///// 符号表达式（边界符与运算符）
        ///// </summary>
        //private static readonly Regex PunctuatorRegex = new Regex($"({PunctuatorPattern})");

        /// <summary>
        /// 数字表达式(非负浮点数)
        /// </summary>
        private static readonly string NumericPattern = $@"(?=\s|\b)(\d+)(\.\d+)?(?=\s|\b)";

        /// <summary>
        /// 标识表达式
        /// </summary>
        private static readonly string IdentifierPattern = $@"(?<=\s|\b)({LetterRegex}|_)(_|{DigitRegex}|{LetterRegex})*(?=\s|\b)";
        
        /// <summary>
        /// 字符串字面量
        /// </summary>
        private static readonly string StringPattern = $@"'.*?'";

        /// <summary> 
        /// 单行注释 这里没有过滤掉字符串字面量
        /// </summary>
        private static readonly string CommentPattern = $@"(/\*(.|\n)*?\*/)|(//.*[\n$])";

        /// <summary>
        /// 空格
        /// </summary>
        private static readonly string WhiteSpacePattern = $@"\s+";
    }
}
