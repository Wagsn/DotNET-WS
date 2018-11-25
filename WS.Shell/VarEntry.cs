﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：VarEntry
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 9:42:03
* 更新时间 ：2018/11/21 9:42:03
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 包含一个变量的完整信息
    /// </summary>
    public class VarEntry
    {
        
        private string raw;  // var raw -> str:string="hello" | var=163511
        private string name;  // var name
        private VarValue value = new VarValue(); // var value

        /// <summary>
        /// 变量名
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// 原始数据，声明及定义
        /// </summary>
        public string Raw { get => raw; set => raw = value; }

        /// <summary>
        /// 变量值（包含类型，值，种类）
        /// </summary>
        public VarValue Value { get => value; set => this.value = value; }
    }

    /// <summary>
    /// 变量值的描述，Variable Value | Value Kind
    /// str:string="hello"  // type = string value = "hello"
    /// num=163511  // type = var  value = 163511 
    /// </summary>
    public class VarValue
    {
       
        private string kind; // var kind boolean number string function
        private Type type;  // var type
        private object value; // var value

        /// <summary>
        /// 值的包装，将各种值包装成object
        /// </summary>
        public object Value { get => value; set => this.value = value; }
        /// <summary>
        /// 值的类型，C#系统标识
        /// </summary>
        public Type Type { get => type; set => type = value; }
        /// <summary>
        /// 值的种类，本系统内部标识
        /// </summary>
        public string Kind { get => kind; set => kind = value; }
    }

    /// <summary>
    /// 数字的值描述
    /// </summary>
    public class Number : VarValue
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public Number()
        {
            Kind = "number";
            Type = typeof(Number);
        }

        public bool _Equals(Number num)
        {
            return false;
        }
    }
}