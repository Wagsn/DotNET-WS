#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：Analyzer
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 10:07:47
* 更新时间 ：2018/11/21 10:07:47
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 语义分析器，用来分析抽象语法树，连接上下文的并执行语法树的工具
    /// </summary>
    public class Analyzer
    {
        public AST AST { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ast"></param>
        public void Init(AST ast)
        {
            AST = ast;
        }

        /// <summary>
        /// 执行整个语法树，需要创建一个新的上下文还是链接整个应用的上下文
        /// </summary>
        public void RunAll()
        {

        }
    }
}
