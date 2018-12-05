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
using System.Text;

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
        /// <param name="sourceCode">源代码文本</param>
        /// <returns></returns>
        public static Token[] Lexing(string sourceCode)
        {
            // 引号开始
            int quotationSstart = -1;
            int letterStart = -1;
            //int spaceStart = -1;
            var trimCode = sourceCode;
            List<Token> tokens = new List<Token>();
            for (int i = 0; i < trimCode.Length; i++)
            {
                // 遇到引号
                if (trimCode[i] == '"')
                {
                    // 开始引号
                    if (quotationSstart == -1)
                    {
                        quotationSstart = i;
                    }
                    // 结束引号
                    else
                    {
                        Token token = new Token
                        {
                            Type = "String",
                            Value = trimCode.Substring(quotationSstart, i - quotationSstart),
                            Range = new int[2] { quotationSstart, i }
                        };
                        tokens.Add(token);
                        quotationSstart = -1;
                    }
                }
                // 遇到制表符及空格
                else if (trimCode[i] == ' ' || trimCode[i] == '\t' || trimCode[i] == '\n' || trimCode[i] == '\r')
                {
                    // 引号未开始
                    if (quotationSstart == -1)
                    {
                        // 字母结束
                        if (letterStart != -1)
                        {
                            Token token = new Token
                            {
                                Type = "Identifier",
                                Value = trimCode.Substring(letterStart, i - letterStart - 1),
                                Range = new int[2] { letterStart, i - 1 }
                            };
                            tokens.Add(token);
                            letterStart = -1;
                        }
                    }
                    // 引号已开始
                    else
                    {
                        continue;
                    }
                }
                // 遇到字母
                else if (trimCode[i] >= 'a' && trimCode[i] <= 'Z')
                {
                    // 引号未开始
                    if (quotationSstart == -1)
                    {
                        if (letterStart == -1)
                        {
                            letterStart = i;
                        }
                    }
                    // 引号已开始
                    else
                    {
                        continue;
                    }
                }
                // 不能包含其它字符
                else
                {
                    throw new Exception("源代码不能包含其它字符");
                }
            }
            return tokens.ToArray();
        }

        public static Token[] Scanner(string source)
        {
            return null;
        }

        private static readonly string LetterRegex = @"[a-zA-Z]";
        private static readonly string DigitRegex = @"[0-9]";  // \d
        private static readonly string IdentifierRegex = $"{LetterRegex}({DigitRegex}|{LetterRegex})*";
        
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
