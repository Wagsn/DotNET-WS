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

namespace WS.Shell
{
    /// <summary>
    /// 单词
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 类型（Keyword：关键字、Identifier：标识符、Punctuator：符号，符号表、Numeric：数字，Literal：字面量）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 值（文本）
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 所在范围 index-base range [start: int, end: int]
        /// </summary>
        public readonly int[] Range = new int[2];

        // Line and column-based  Location(start: Position(line: int, column: int), end: Position(line: int, column: int))

        public readonly Location loc = new Location();
    }

    /// <summary>
    /// 位置区间
    /// </summary>
    public class Location
    {
        public Position start;
        public Position end;
    }

    /// <summary>
    /// 位置坐标
    /// </summary>
    public class Position
    {
        public int line;
        public int column;
    }
}
