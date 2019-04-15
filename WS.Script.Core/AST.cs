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

namespace WS.Script.Core
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
        public int TokenIndex { get; set; }

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

        ///// <summary>
        ///// 访问节点，
        ///// 如果该节点是一个定义表达式，将会在上下文中添加一个定义的单元
        ///// 如果是一个函数调用表达式，将会执行该函数，引入上下文
        ///// </summary>
        ///// <param name="context"></param>
        //public void Visit(RunContext context)
        //{

        //}

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
        /// <summary>
        /// 语句体
        /// </summary>
        public List<ASTNode> Body { get; set; }

        public ProgramASTNode()
        {
            Type = "Program";
        }
    }

    /// <summary>
    /// 表达式节点
    /// </summary>
    public class ExpressionASTNode : ASTNode
    {

    }

    /// <summary>
    /// 语句节点
    /// </summary>
    public class StatementASTNode : ASTNode
    {

    }

    /// <summary>
    /// 单目表达式
    /// </summary>
    public class UnaryExpression : ExpressionASTNode
    {
        public string Operator { get; set; }

        public bool Prefix { get; set; }

        public ExpressionASTNode Argument { get; set; }

        public UnaryExpression()
        {
            Type = "UnaryExpression";
        }
    }

    /// <summary>
    /// 双目表达式
    /// 左结合（优先级：() > *|/ > +|-）
    /// </summary>
    public class BinaryExpression : ExpressionASTNode
    {
        public string Operator { get; set; }

        public ExpressionASTNode Left { get; set; }

        public ExpressionASTNode Right { get; set; }

        public BinaryExpression()
        {
            Type = "BinaryExpression";
        }
    }

    /// <summary>
    /// 赋值表达式
    /// 右结合（a=b=c）==（a=(b=c)）
    /// </summary>
    public class AssignmentExpression : BinaryExpression
    {
        public AssignmentExpression()
        {
            Type = "AssignmentExpression";
            Operator = "=";
        }
    }

    /// <summary>
    /// 表达式语句
    /// </summary>
    public class ExpressionStatement : StatementASTNode
    {
        /// <summary>
        /// 表达式
        /// </summary>
        public ExpressionASTNode Expression { get; set; }

        public ExpressionStatement()
        {
            Type = "ExpressionStatement";
        }
    }

    /// <summary>
    /// 调用表达式
    /// </summary>
    public class CallExpression : ExpressionASTNode
    {
        /// <summary>
        /// 调用者表达式
        /// </summary>
        public ExpressionASTNode Callee { get; set; }

        /// <summary>
        /// 参数表达式
        /// </summary>
        public List<ExpressionASTNode> Arguments { get; set; }

        public CallExpression()
        {
            Type = "CallExpression";
        }
    }

    /// <summary>
    /// 成员表达式
    /// </summary>
    public class MemberExpression :ExpressionASTNode
    {
        /// <summary>
        /// 对象
        /// </summary>
        public ExpressionASTNode Object { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public ExpressionASTNode Property { get; set; }

        /// <summary>
        /// 属性是否需要计算
        /// 如果是ID则为false，其它为true
        /// </summary>
        public bool Computed { get; set; }

        public MemberExpression()
        {
            Type = "MemberExpression";
        }
    }

    /// <summary>
    /// 数组表达式
    /// </summary>
    public class ArrayExpression : ExpressionASTNode
    {
        public List<ExpressionASTNode> Elements { get; set; } = new List<ExpressionASTNode>();
        
        public ArrayExpression()
        {
            Type = "ArrayExpression";
        }
    }

    public class FunctionExpression :ExpressionASTNode
    {
        public Identifier Id { get; set; }

        public List<ExpressionASTNode> Params { get; set; }

        public StatementASTNode Body { get; set; }
    }

    /// <summary>
    /// 箭头函数
    /// Body如果是表达式，则表达式为返回值，如果为块域，则return语句作为返回值
    /// </summary>
    public class ArrowFunctionExpression : FunctionExpression
    {
    }

    /// <summary>
    /// IF语句
    /// </summary>
    public class IfStatement : StatementASTNode
    {
        /// <summary>
        /// condition
        /// </summary>
        public ExpressionASTNode Test { get; set; }

        /// <summary>
        /// if
        /// </summary>
        public StatementASTNode Consequent { get; set; }

        /// <summary>
        /// else
        /// </summary>
        public StatementASTNode Alternate { get; set; }
    }

    /// <summary>
    /// 语句块
    /// </summary>
    public class BlockStatement : StatementASTNode
    {
        public List<ASTNode> Body { get; set; }

        public BlockStatement()
        {
            Type = "BlockStatement";
        }
    }

    /// <summary>
    /// 返回语句
    /// </summary>
    public class ReturnStatement :StatementASTNode
    {
        public ExpressionASTNode Argument { get; set; }

        public ReturnStatement()
        {
            Type = "ReturnStatement";
        }
    }

    /// <summary>
    /// 变量申明表达式
    /// </summary>
    public class VariableDeclarator : ASTNode
    {
        public Identifier Id { get; set; }

        public ExpressionASTNode Init { get; set; }
        
        public VariableDeclarator()
        {
            Type = "VariableDeclarator";
        }
    }

    public class FunctionDeclaration : StatementASTNode
    {
        public Identifier Id { get; set; }

        public List<ExpressionASTNode> Params { get; set; }

        public BlockStatement Body { get; set; }

        public FunctionDeclaration()
        {
            Type = "FunctionDeclaration";
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

    /// <summary>
    /// ID，具体值的符号表示，可能是字面量，函数申明式（fun: Void=>Void），表达式
    /// </summary>
    public class Identifier: ExpressionASTNode
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

    public class Numeric : ExpressionASTNode
    {
        public object Value { get; set; }

        public Numeric()
        {
            Type = "Numeric";
        }
    }
}
