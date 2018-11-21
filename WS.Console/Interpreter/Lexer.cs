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
    /// 词法分析器
    /// </summary>
    class Lexer
    {
        /// <summary>
        /// Lexing Lexical(词汇)
        /// </summary>
        /// <param name="sourceCode">源代码文本</param>
        /// <returns></returns>
        public static Token[] Lexing(string sourceCode)
        {
            return new Token[0];
        }

        private static readonly string LetterRegex = @"[a-zA-Z]";
        private static readonly string DigitRegex = @"[0-9]";  // \d
        private static readonly string IdentifierRegex = $"{LetterRegex}({DigitRegex}|{LetterRegex})*";

    }
}
