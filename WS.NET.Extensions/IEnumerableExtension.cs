using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// 枚举类扩展
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 去重（去掉为空的元素）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="comparer">比较表达式</param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> enumerable, Func<T, T, bool> comparer)
        {
            if (enumerable == null || enumerable.Count() <= 1) return enumerable;
            return enumerable.Where(t => t != null && t.Equals(enumerable.First(b => comparer(t, b))));
        }

        /// <summary>
        /// 过滤(排除)满足条件的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (enumerable == null || !enumerable.Any()) return enumerable;
            return enumerable.Where(t => !predicate(t));
        }

        /// <summary>
        /// 去空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<T> TrimEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Filter(t => t == null);
        }

        /// <summary>
        /// 去空(去除空引用字符串(null)、空字符串(string.Empty)、空格字符串(WhiteSpace))
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<string> TrimEmpty(this IEnumerable<string> enumerable)
        {
            if (enumerable == null || !enumerable.Any()) return enumerable;
            return enumerable.Filter(string.IsNullOrWhiteSpace);
        }

        /// <summary>
        /// 对集合进行合并操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="merge">合并表达式</param>
        /// <returns></returns>
        public static T Reduce<T>(this IEnumerable<T> enumerable, Func<T, T, T> merge)
        {
            if (enumerable == null || !enumerable.Any()) return default(T);
            var reslut = enumerable.First();
            foreach(var item in enumerable.Skip(1))
            {
                reslut = merge(reslut, item);
            }
            return reslut;
        }

        /// <summary>
        /// 对集合进行合并操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="merge">合并表达式(total, currentValue)=>total, 第一次计算时(initialValue, first)</param>
        /// <param name="initialValue">初始值，参与合并运算</param>
        /// <returns></returns>
        public static R Reduce<T,R>(this IEnumerable<T> enumerable, Func<R, T, R> merge, R initialValue)
        {
            if (enumerable == null || !enumerable.Any()) return initialValue;
            var reslut = initialValue;
            foreach (var item in enumerable)
            {
                reslut = merge(reslut, item);
            }
            return reslut;
        }

        /// <summary>
        /// 移除满足条件的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> RemoveWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Except(enumerable.Where(predicate));
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> enumerable, IEnumerable<T> other, Func<T, T, bool> comparison)
        {
            var otherOwn = other.Where(a => enumerable.All(b => !comparison(a, b)));
            var result = enumerable.ToList();
            result.AddRange(otherOwn);
            return result;
        }
    }
}
