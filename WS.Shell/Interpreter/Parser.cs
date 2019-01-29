#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：Parser
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 9:50:58
* 更新时间 ：2018/11/21 9:50:58
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 语法分析器(syntactic analyzer)
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// 语法分析
        /// </summary>
        /// <param name="tokens">单词流</param>
        /// <returns></returns>
        public AST Parse (List<Token> tokens)
        {
            for(int i=0; i< tokens.Count; i++)
            {
                
            }
            return null;
        }

        /// <summary>
        /// 直接解析源代码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public AST Parse (string source)
        {
            return Parse(Lexer.Lexing(source));
        }

        //public AST Syntax(Token[] tokens)
        //{
        //    return null;
        //}
    }
}
