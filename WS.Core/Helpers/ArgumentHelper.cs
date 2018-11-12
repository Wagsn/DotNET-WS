using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Helpers
{
    /// <summary>
    /// 参数验证助手
    /// </summary>
    public static class ArgumentHelper
    {
        /// <summary>
        /// 空检查，暂时不知道参数注解（特性）怎么用
        /// </summary>
        /// <typeparam name="TArgument"></typeparam>
        /// <param name="arg"></param>
        public static void CheckNull<TArgument>(TArgument arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg));
            }
        }
    }
}