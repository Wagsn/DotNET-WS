#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：AST
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 9:50:15
* 更新时间 ：2018/11/21 9:50:15
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 抽象语法树（Abstract syntax tree）
    /// </summary>
    public class AST
    {
        /// <summary>
        /// 根节点
        /// </summary>
        public ASTNode Root { get; set; }
    }

    /// <summary>
    /// 语法树节点
    /// </summary>
    public class ASTNode
    {
        ///// <summary>
        ///// 名称（person, marth）
        ///// </summary>
        //public string Name { get; set; }

        ///// <summary>
        ///// 描述（语义相关的：如const，）
        ///// </summary>
        //public string Kind { get; set; }

        /// <summary>
        /// 节点类型（语法树相关的，如：Program）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 源代码位置信息
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// 访问节点，
        /// 如果该节点是一个定义表达式，将会在上下文中添加一个定义的单元
        /// 如果是一个函数调用表达式，将会执行该函数，引入上下文
        /// </summary>
        /// <param name="context"></param>
        public void Visit(RunContext context)
        {

        }

        /// <summary>
        /// 接收一个访问器
        /// </summary>
        /// <param name="visitor"></param>
        public void Accept(ASTVisitor visitor)
        {

        }
    }

    /// <summary>
    /// 程序节点，根节点
    /// </summary>
    public class ProgramASTNode : ASTNode
    {
        public List<ASTNode> Body { get; set; }

        public ProgramASTNode()
        {
            Type = "Program";
        }
    }

    /// <summary>
    /// 语句节点
    /// </summary>
    public class StatementASTNode : ASTNode
    {

    }

    /// <summary>
    /// 表达式节点
    /// </summary>
    public class ExpressionASTNode : ASTNode
    {

    }
    
    public class VariableDeclarator : ASTNode
    {
        /// <summary>
        /// 
        /// </summary>
        public VariableDeclarator()
        {
            Type = "VariableDeclarator";
        }
    }

    /// <summary>
    /// https://help.eclipse.org/neon/index.jsp?topic=%2Forg.eclipse.jdt.doc.isv%2Freference%2Fapi%2Forg%2Feclipse%2Fjdt%2Fcore%2Fdom%2FASTVisitor.html
    /// 
    /// </summary>
    public class ASTVisitor
    {
        public bool Visit(ASTNode node)
        {
            return false;
        }

        public void PreVisit(ASTNode node)
        {

        }
        public void EndVisit(ASTNode node)
        {

        }
        public void PostVisit(ASTNode node)
        {

        }
    }

    public class Identifier: ASTNode
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public Identifier()
        {
            Type = "Identifier";
        }
    }
}
