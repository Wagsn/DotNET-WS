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
using WS.Shell.CmdUnit;

namespace WS.Shell
{
    /// <summary>
    /// 包含一个变量的完整信息
    /// 变量名与变量值
    /// 以及在源代码中的位置
    /// </summary>
    public class VarEntry
    {
        /// <summary>
        /// 变量名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 变量值（包含类型，值，种类）
        /// </summary>
        public VarData Data { get; set; } = new VarData();

        /// <summary>
        /// 类型
        /// </summary>
        public TypeInfo Type { get; set; }

        /// <summary>
        /// 原始数据，声明及定义
        /// </summary>
        public string Raw { get; set; }
    }

    /// <summary>
    /// 变量值的描述
    /// Name，Data，Type，Kind，Sign，Raw，Set，Get，Run
    /// </summary>
    public class VarData : ScriptContext
    {
        /// <summary>
        /// 值名（如函数名）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值的包装，将各种值包装成object
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 值的类型，C#系统标识
        /// </summary>
        public TypeInfo Type { get; set; }

        /// <summary>
        /// 值的种类，本系统内部标识，如Number，String
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// 标签（"String=>Void|Void=>String"）
        /// </summary>
        public SignInfo Sign { get; set; }

        /// <summary>
        /// 原始代码（定义的代码，如：print: String=>Void{ [native code] }）
        /// mult: (a: Number, b: Number)=>Number{ c: Number := 0; whiledo(condition: Void=>Bool { return  }, do); }
        /// </summary>
        public string Raw { get; set; }

        public static VarData New (double o)
        {
            return new NumberData
            {
                Data = o
            };
        }

        public static VarData New (string o)
        {
            return new StringData
            {
                Data = o
            };
        }

        /// <summary>
        /// p.age = 23;
        /// </summary>
        /// <param name="args">参数</param>
        public virtual void Set(VarData[] args)
        {
            throw new NotImplementedException("没有实现");
        }

        /// <summary>
        /// var a = p.age;
        /// </summary>
        /// <returns></returns>
        public virtual VarData Get()
        {
            throw new NotImplementedException("没有实现");
        }

        /// <summary>
        /// var e = p.eat("food");
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public virtual VarData Run(VarData[] args)
        {
            Set(args);
            return Get();
        }
    }

    /// <summary>
    /// 函数实例
    /// </summary>
    public class FunData : VarData
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="caller">调用上下文</param>
        /// <param name="args">实际参数</param>
        /// <returns></returns>
        public VarData Run(VarData caller, VarData[] args)
        {
            // 将调用上下文(CallContext.VarTable)和实参保存在函数上下文(VarTable)中
            // 还有父类上下文(BaseContext.VarTable)和外层上下文(OutContext.VarTable)
            // 局部上下文(LocalContext.VarTable)

            return null;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args">实际参数</param>
        /// <returns></returns>
        public override VarData Run(VarData[] args)
        {
            // 变量表-添加形参（从类型信息中获取形参信息：）
            VarTable.Add(new VarEntry
            {
                Data = new VarData
                {

                }
            });
            // 变量表-保存实参
            VarTable.Add(new VarEntry
            {
                Name = "argumnets",
                //Raw = "argumnets",
                Data = new VarData
                {
                    Sign = new SignInfo
                    {
                        IsUnit = true,
                        Name = "List<Object>"
                    }
                }
            });
            // 计算

            // 返回值
            return new VarData
            {

            };
        }
    }

    /// <summary>
    /// 数值
    /// </summary>
    public class NumberData : VarData
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public NumberData()
        {
            Kind = "Number";
            //Type = typeof(NumberData);
        }
    }

    /// <summary>
    /// 字符串
    /// </summary>
    public class StringData : VarData
    {
        public StringData()
        {
            Kind = "String";
            //Type = typeof(StringData);
        }
    }

    /// <summary>
    /// 类型信息
    /// Person: Object {
    ///     Address: Object {
    ///         // address = "Hb Village, Hb Town, Kx County";
    ///         // setter of Address.
    ///         set: (val:String)=>Void {
    ///             // 切割字符串,从小到大, Hb Village, Hb Town, Kx County
    ///             strs: List<string> := val.split(",");
    ///             village:= strs[0].split(" ")[0];
    ///             town:= strs[2].split(" ")[0];
    ///             county:= strs[4].split(" ")[0];
    ///         };
    ///         // getter of Address.
    ///         get: Void=>String {
    ///             return $"{village} Village, {town} Town, {county} County";
    ///         }
    ///         county: String;  // 县
    ///         town: String;  // 镇
    ///         village: String;  // 村
    ///     };
    ///     name: String := "wagsn";
    ///     age: Number := 23;
    ///     address: Address := Address(){
    ///         county:= "kx",
    ///         town:= "hb",
    ///         village:= "hb"
    ///     };
    /// };
    /// </summary>
    public class TypeInfo
    {
        /// <summary>
        /// 类型名(Person)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型签名（函数签名为类型名组装而成，如：String=>Void）(Object)
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 基类类型信息
        /// </summary>
        public TypeInfo BaseType { get; set; }
        
        /// <summary>
        /// 成员类型信息
        /// </summary>
        // getters setters menbers
        public List<TypeInfo> Members { get; set; }
    }

    /// <summary>
    /// 签名信息
    /// </summary>
    public class SignInfo
    {
        /// <summary>
        /// 名称（如："String=>Void"）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 输入签名
        /// </summary>
        public SignInfo Input { get; set; }

        /// <summary>
        /// 输出签名
        /// </summary>
        public SignInfo OutPut { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public TypeInfo UnitType { get; set; }

        /// <summary>
        /// 是否原子（原子类型的输入与输出一致）
        /// </summary>
        public bool IsUnit { get; set; }
    }

    /// <summary>
    /// 实例对象信息
    /// </summary>
    public class ObjectInfo
    {

    }

    /// <summary>
    /// 对象类型实例
    /// </summary>
    public class ObjectType
    {

    }

    /// <summary>
    /// 表达式
    /// </summary>
    public class Expr
    {

    } 

    /// <summary>
    /// 语句体
    /// </summary>
    public class Stat
    {

    }

    /// <summary>
    /// 执行块
    /// </summary>
    public class Prog { }
}
