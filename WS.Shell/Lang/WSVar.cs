using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.Lang
{
    /// <summary>
    /// 变量
    /// </summary>
    class WSVar<TValue>
    {
        /// <summary>
        /// 值
        /// </summary>
        TValue val;

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type{ get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public TValue Val
        {
            set
            {
                val = value;
            }
            get
            {
                return val;
            }
        }

        /// <summary>
        /// 设值
        /// </summary>
        /// <param name="var"></param>
        public void Set(WSVar<TValue> var)
        {
            Val = var.Val;
        }

        /// <summary>
        /// 设值
        /// </summary>
        /// <param name="obj"></param>
        public void Set(TValue obj)
        {
            Val = obj;
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <returns></returns>
        public WSVar<TValue> Get()
        {
            return new WSVar<TValue>
            {
                Val=val
            };
        }



    }

    class WSVar : WSVar<object> { }
}
