using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Object转IDictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static System.Collections.Generic.IDictionary<string, object> ToDictionary(this object obj) => WS.NET.Extensions.MapperHelper.ObjectToDictionary(obj);

        /// <summary>
        /// 转化成整数
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int ToInteger<T>(this T obj)
        {
            if (obj == null) return 0;
            var src = obj.ToString();
            if (src.Any(c => c < 48 || c > 57)) throw new FormatException("该字符串不是整数字符串");
            return src.Select(c => c - 48).Reduce((x, y) => x * 10 + y);
        }
    }
}
