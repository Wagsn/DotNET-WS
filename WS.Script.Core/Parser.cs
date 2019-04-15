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

namespace WS.Script.Core
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
            int currIndex = 0;
            var ast = new ProgramASTNode();
            ast.Type = "Program";
            ast.Body = new List<ASTNode>();
            while (currIndex < tokens.Count)
            {
                ast.Body.Add(NextStatement(ref currIndex, tokens));
            }
            return null;
        }

        private ExpressionASTNode NextExpression(ref int currIndex, List<Token> tokens)
        {
            var token = tokens[currIndex];
            if (token.Type== "Numeric")
            {
                return new Numeric
                {
                    Value = token.Value
                };
            }
            if(token.Type == "String")
            {
                return null;
            }

            return null;
        }

        public StatementASTNode NextStatement(ref int currIndex, List<Token> tokens)
        {
            return null;
        }

        public static void Test()
        {
            FunctionDeclaration declaration = new FunctionDeclaration
            {
                Id = new Identifier
                {
                    Name = "add"
                },
                Params = new List<ExpressionASTNode>
                {
                    new Identifier
                    {
                        Name = "a"
                    },
                    new Identifier
                    {
                        Name = "b"
                    }
                },
                // ArrowFunctionExpression
                Body = new BlockStatement  // 有return语句
                {
                    Body = new List<ASTNode>
                    {
                        new ReturnStatement
                        {
                            Argument = new BinaryExpression
                            {
                                Operator = "+",
                                Left = new Identifier
                                {
                                    Name = "a"
                                },
                                Right = new Identifier
                                {
                                    Name = "b"
                                }
                            }
                        }
                    }
                }
            };
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
