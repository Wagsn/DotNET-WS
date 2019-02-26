#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：Token
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 10:25:09
* 更新时间 ：2018/11/21 10:25:09
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Script.Core
{
    /// <summary>
    /// 单词
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 类型(描述语法树相关的类型)（Keyword：关键字，保留字、Identifier：标识符、Punctuator：符号(界符，如："}"，运算符，如："+")，符号表、Numeric：数值字面量，Literal：字面量（String：字符串字面量，Boolean：布尔字面量，None：空字面量））
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 值（文本，如："=", "1236", "||", "\"name\"", "print"）
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 类型
        /// ADD + add, SUB - subtract, MUL * multiply, DIV / divide, 
        /// LCB { left curly bracket, RCB }, 
        /// LP ( Left parenthesis, RP ), LSB [ left square bracket, RSB ],
        /// SEM ; semicolon
        /// ID, UNK Unknown
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// 所在范围 index-base range [start: int, end: int]
        /// </summary>
        public Location Loc { get; set; }
    }

    /// <summary>
    /// 位置区间
    /// </summary>
    public class Location
    {
        /// <summary>
        /// 开始位置
        /// </summary>
        public Position Start { get; set; }

        /// <summary>
        /// 结束位置
        /// </summary>
        public Position End { get; set; }

        /// <summary>
        /// 字符流范围
        /// </summary>
        public Range Range { get; set; }
    }

    /// <summary>
    /// 在源代码中的范围
    /// </summary>
    public class Range
    {
        /// <summary>
        /// 源代码字符串流开始索引
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 源代码字符串流结束索引
        /// </summary>
        public int End { get; set; }
    }

    /// <summary>
    /// 位置坐标
    /// </summary>
    public class Position
    {
        /// <summary>
        /// 所在行，从0开始
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// 所在列，从0开始
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// 源代码中偏移量，从0开始
        /// </summary>
        public int Offset { get; set; }
    }
}
