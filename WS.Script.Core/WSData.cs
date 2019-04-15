using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Script.Core
{
    /// <summary>
    /// Wagsn Script Data
    /// </summary>
    public class WSData
    {

    }

    /// <summary>
    /// 有值的
    /// </summary>
    public class WSValue : WSData
    {
    }

    public class WSObject :WSValue
    {
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(WSValue key, WSValue value)
        {
            return false;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public WSValue Get(WSValue key)
        {
            return null;
        }

        /// <summary>
        /// 存在值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Has(WSValue key)
        {
            return false;
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(WSValue key)
        {
            return false;
        }

        /// <summary>
        /// 作为函数使用
        /// </summary>
        /// <param name="recv"></param>
        /// <param name="argv"></param>
        /// <returns></returns>
        public WSValue Run(WSValue recv, List<WSValue> argv)
        {
            return null;
        }

    }

    public class WSArray
    {
        /// <summary>
        /// 数组的长度
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            return 0;
        }

        /// <summary>
        /// 取出在某个位置的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public WSObject ElementAt(int index)
        {
            return null;
        }
    }
}
