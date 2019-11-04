using System;
using System.Collections.Generic;

namespace WS.NET.Extensions
{
    /// <summary>
    /// 类型给映射助手
    /// </summary>
    public class MapperHelper
    {
        /// <summary>
        /// 对象转字典
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="exceptKeys">不生成字典Key的属性列表</param>
        /// <returns></returns>
        public static IDictionary<string, object> ObjectToDictionary(object obj, List<string> exceptKeys = null)
        {
            //var dic = Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelper.ObjectToDictionary(obj);
            //var dic2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Newtonsoft.Json.JsonConvert.SerializeObject(obj));

            Dictionary<string, object> pairs = new Dictionary<string, object>();
            if (exceptKeys == null)
                exceptKeys = new List<string>();

            var t = obj.GetType();
            var fs = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance);
            foreach(var p in fs)
            {
                try
                {
                    pairs[p.Name] = p.GetValue(obj);
                }
                catch
                {
                    continue;
                }
            }
            return pairs;
        }

        /// <summary>
        /// 修改类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        static private Object ChangeType(object value, Type targetType)
        {
            Type convertType = targetType;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(targetType);
                convertType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertType);
        }
    }
}
