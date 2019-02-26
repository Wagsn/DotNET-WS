using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Script.Core
{
    class Test
    {
    }

    /// <summary>
    /// 脚本对象描述
    /// 他的实例就是一个脚本对象描述
    /// </summary>
    public class ObjectInfo
    {

    }

    /// <summary>
    /// 脚本类型描述
    /// 它的实例就是一个脚本类型描述
    /// </summary>
    public class ClassInfo
    {
        /// <summary>
        /// 根据类型描述生成对象描述
        /// </summary>
        /// <returns></returns>
        public ObjectInfo NewInstance()
        {
            return new ObjectInfo();
        }
    }
}
