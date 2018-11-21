#region << 版 本 注 释 >>
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
    /// 包含变量的值部分，Variable Value
    /// str:string="hello"  // type = string value = "hello"
    /// num=163511  // type = var  value = 163511 
    /// </summary>
    public class VarValue
    {
        public Type type;  // var type
        public object value; // var value
    }

    /// <summary>
    /// 包含一个变量的完整信息
    /// </summary>
    public class VarEntry
    {
        public string raw;  // var raw -> str:string="hello" | var=163511
        public string name;  // var name
        public VarValue value; // var value
    }
}
