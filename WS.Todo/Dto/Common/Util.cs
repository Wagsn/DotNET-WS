using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Dto
{
    /// <summary>
    /// TODO：将其迁移到专有工具集中，工具类，可能会被拆分至各个Helper中
    /// </summary>
    public static class Util
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
