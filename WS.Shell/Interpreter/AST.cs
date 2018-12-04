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
        
    }

    /// <summary>
    /// 语法树节点
    /// </summary>
    public class ASTNode
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 访问节点，
        /// 如果该节点是一个定义表达式，将会在上下文中添加一个定义的单元
        /// 如果是一个函数调用表达式，将会执行该函数，引入上下文
        /// </summary>
        /// <param name="context"></param>
        public void Visit(RunContext context)
        {

        }
    }


}
