#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell
* 项目描述 ：
* 类 名 称 ：BlockContext
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 2:36:21
* 更新时间 ：2018/11/22 2:36:21
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 运行时上下文环境
    /// </summary>
    public class RunContext
    {
        /// <summary>
        /// 变量表，封装CS变量，保存单元的变量，如返回值，参数值
        /// </summary>
        public Map<VarValue> VarMap { get; set; }

        /// <summary>
        /// 单元表，保存域中的单元
        /// </summary>
        public Map<Unit> UnitMap { get; set; }
        
        protected RunContext() { }

        /// <summary>
        /// 在上下文中添加单元
        /// </summary>
        /// <param name="name"></param>
        /// <param name="unit"></param>
        public virtual void Set(string name, Unit unit)
        {
            UnitMap.Set(name, unit);
        }

        /// <summary>
        /// 获取此类实例
        /// </summary>
        /// <returns></returns>
        public static RunContext New()
        {
            return new RunContext
            {
                VarMap = new Map<VarValue>(),
                UnitMap = new Map<Unit>()
            };
        }
    }

    /// <summary>
    /// 映射表
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Map<TValue>
    {
        private Dictionary<string, TValue> Pairs { get; set; }

        /// <summary>
        /// 移出
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue Remove(string key)
        {
            TValue value = default(TValue);
            if (Pairs.ContainsKey(key))
            {
                value = Pairs[key];
                Pairs.Remove(key);
            }
            return value;
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool Contains(string key)
        {
            return Pairs.ContainsKey(key);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue Get(string key)
        {
            return Pairs[key];
        }

        ///// <summary>
        ///// 索引器
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public TValue this[string key]
        //{
        //    set
        //    {
        //        Set(key, value);
        //    }
        //    get
        //    {
        //        return Get(key);
        //    }
        //}

        /// <summary>
        /// 获取值或默认值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue GetOrDefault(string key)
        {
            return Pairs.GetValueOrDefault(key);
        }

        /// <summary>
        /// 设置
        /// 如果不存在将返回true
        /// 如果存在将返回false
        /// 参数非法将抛出异常
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool Set(string key, TValue value)
        {
            if (Pairs.ContainsKey(key))
            {
                Pairs[key] = value;
                return false;
            }
            else
            {
                Pairs.Add(key, value);
                return true;
            }
        }
    }

    /// <summary>
    /// 单元，统一表示函数与变量，左值右值，以及其它类型的数据
    /// 不包含单元名，是为了重复使用，从外层不能反向查找内层
    /// </summary>
    public class Unit : RunContext
    {
        /// <summary>
        /// 是否被初始化，调用Init函数会初始化，第一次Set也会
        /// </summary>
        public bool IsInit { get; protected set; }

        /// <summary>
        /// 是否被执行
        /// </summary>
        public bool IsRan { get; protected set; }

        /// <summary>
        /// 单元名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 单元外部的上下文
        /// </summary>
        protected virtual Unit Scope { get; set; } 

        /// <summary>
        /// 多参数对象，用于显式传参
        /// </summary>
        protected virtual Map<Unit> Arguments { get; set; }
        
        /// <summary>
        /// 创建此类的实例
        /// </summary>
        /// <returns></returns>
        public static Unit New(Unit scope)
        {
            // 如果context为空，生成默认上下文
            // 不为空，则为当前单元设置外层上下文
            return new Unit
            {
                IsInit = false,
                IsRan = false,
                Scope = scope,
                VarMap = new Map<VarValue>(),
                UnitMap = new Map<Unit>()
            };
        }

        /// <summary>
        /// 设置当前Unit值的参数，这里将不会立即执行
        /// 将会忽略Unit的Name属性
        /// </summary>
        /// <param name="args"></param>
        public virtual void Set(params Unit[] args)
        {
            // 如果传入的是字符串
            if (args!=null&&args.Length>=1&&args[0].GetValue().Value is string)
            {
                var code = args[0].GetValue().Value as string;
                VarMap.Set("source", VarValue.New(code));  // source 用来保存源代码以及所在的位置，这里暂时只保存源代码
                IsInit = true;
            }
            else
            {
                throw new NotImplementedException("Set 函数没有实现");
            }
        }

        /// <summary>
        /// 取值，如果值未进行计算将计算之后返回值
        /// </summary>
        /// <returns></returns>
        public virtual Unit Get()
        {
            if (IsInit)
            {
                // 解析
                // 获取单词流 Token[]
                // 生成语法树 AST
                // 遍历语法树 Visitor
                // 在这里简单读取Print函数 print "hello world"
                string code = VarMap.Get("source").Value.ToString();

                Token[] tokenArr = Lexer.Lexing(code);

                for (int i=0; i<tokenArr.Length; i++)
                {
                    if(tokenArr[i].Type == "String")
                    {
                        
                    }
                    else if(tokenArr[i].Type == "Identifier")
                    {

                    }
                    else
                    {
                        throw new NotSupportedException("不支持的Token");
                    }
                }

            }

            throw new NotImplementedException("Get 函数没有实现");
        }

        /// <summary>
        /// 通过参数执行，相当于Get与Set的综合
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual Unit Run(params Unit[] args)
        {
            return VoidUnit.New();
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public VarValue GetValue(Unit unit)
        {
            if (unit.IsInit)
            {
                if (unit.IsRan)
                {
                    return unit.Scope.VarMap.Get("returnvalue");
                }
                return unit.Get().Scope.VarMap.Get("returnvalue");
            }
            else
            {
                throw new Exception("No initialization exception");
            }
        }

        public VarValue GetValue()
        {
            if (IsInit)
            {
                if (IsRan)
                {
                    return Scope.VarMap.Get("returnvalue");
                }
                return Get().Scope.VarMap.Get("returnvalue");
            }
            else
            {
                throw new Exception("No initialization exception");
            }
        }

        public TValue TypeValue<TValue>()
        {
            return (TValue)Scope.VarMap.Get("returnvalue").Value;
        }
    }

    public class WString : ValueUnit
    {
        /// <summary>
        /// 创建此类实例
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WString New(string value)
        {
            Unit context = Unit.New(null);
            context.VarMap.Set("argument", VarValue.New(value));
            context.VarMap.Set("returnvalue", VarValue.New(value));
            return new WString
            {
                IsInit = true,
                IsRan = true,
                Scope = context
            };
        }
    }

    public class WNumber : ValueUnit
    {
        public static WNumber New (double d)
        {
            Unit context = Unit.New(null);
            context.VarMap.Set("argument", VarValue.New(d));
            context.VarMap.Set("returnvalue", VarValue.New(d));
            return new WNumber
            {
                IsInit = true,
                IsRan = true,
                Scope = context
            };
        }
    }

    public class VoidUnit : Unit
    {
        /// <summary>
        /// 模板生成
        /// </summary>
        /// <returns></returns>
        public new static VoidUnit New()
        {
            return new VoidUnit();
        }
    }

    /// <summary>
    /// 值单元，与脚本对应
    /// </summary>
    public class ValueUnit : Unit
    {
        public static ValueUnit New(VarEntry[] args =null)
        {
            ValueUnit unit = new ValueUnit
            {
                IsInit = false,
                IsRan = false,
                Scope = Unit.New(null)
            };
            return unit;
        }
        public static ValueUnit New (VarValue value)
        {
            //Run

            ValueUnit unit = new ValueUnit
            {
                IsInit = false,
                IsRan = false,
                Scope = Unit.New(null)
            };
            return unit;
        }

        /// <summary>
        /// 存值
        /// </summary>
        /// <param name="unit"></param>
        public override void Set(params Unit[] args)
        {
            //Context.VarMap.Set("Arg", GetValue(unit));
            IsInit = true;
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <returns></returns>
        public override Unit Get()
        {
            return this;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public override Unit Run(params Unit[] args) // 实参列表
        {
            if (args == null || args.Length==0)
            {
                return null;
            }
            else
            {
                //// 参数暂存
                //for(int i=0; i< Args.Length; i++)
                //{
                //    if (args.Length > i)
                //    {
                //        Args[i].Value=args[i];
                //    }
                //}
                //Value = GetValue(args[0]);
                IsInit = true;
                return this;
            }
        }
    }

    /// <summary>
    /// 打印函数对象，将作为主Context的函数
    /// </summary>
    public class PrintUnit : Unit
    {
        public override Unit Run(params Unit[] args)
        {
            // 参数解析
            VarValue[] values = new VarValue[args.Length];
            for(int i = 0; i< values.Length; i++)
            {
                values[i] = args[i].GetValue();
            }
            // 缓存参数
            VarMap.Set("arguments", new VarValue
            {
                Type = values.GetType(),
                Kind = "Array[Unit]",
                Value = values
            });
            // 函数执行
            return base.Run(args);
        }
    }

    public class FuncUnit : Unit
    {
        public override Unit Run(params Unit[] args)
        {
            // 参数缓存

            return null;
        }
    }

    public static class Test
    {
        public static void Main()
        {
            VarEntry entry = new VarEntry
            {
                Value = new VarValue
                {
                    Type = "string".GetType(),
                    Value = "string"
                }
            };

            Unit unit = new ValueUnit();

            //unit.Set();
        }
    }
}
