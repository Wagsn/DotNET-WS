using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.Lang
{
    /// <summary>
    /// 数字
    /// </summary>
    class WSNumber : WSVar<double>
    {
        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public WSNumber Add(WSNumber number)
        {
            return new WSNumber
            {
                Val = Val + number.Val
            };
        }
    }
}
