using System.Collections.Generic;
using System.Linq;

namespace System
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    static public class StringExtension
    {
        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ToLowerFirst(this string src) => src.Substring(0, 1).ToLower(Globalization.CultureInfo.CurrentCulture) + src.Substring(1);

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ToUpperFirst(this string src) => src.Substring(0, 1).ToUpper(Globalization.CultureInfo.CurrentCulture) + src.Substring(1);

        /// <summary>
        /// 转化成整数
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int ToInteger(this string src)
        {
            //Convert.ToInt32();
            if (src.Any(c => c < 48 || c > 57)) throw new FormatException("该字符串不是整数字符串");
            return src.Select(c => c - 48).Reduce((x, y) => x * 10 + y);
        }
    }
}
