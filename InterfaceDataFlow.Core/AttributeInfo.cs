using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceDataFlow
{
    /// <summary>
    /// 属性
    /// </summary>
    public class AttributeInfo
    {
        /// <summary>
        /// 名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 字段类型（Number、Bool、Object、String、Array）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// JSON: $"\"{name}\":{value}"
        /// </summary>
        public string Json { get => $"\"{Name}\":{Value}"; }

        /// <summary>
        /// JSON Object: $"{{{attributes.Select(a => a.Json).Reduce((x, y) => x + "," + y)}}}"
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static string ToJson(IEnumerable<AttributeInfo> attributes) => $"{{{attributes.Select(a => a.Json).Reduce((x, y) => x + "," + y)}}}";

        /// <summary>
        /// JSON Array: $"[{attributes.Select(a => $"{{{a.Select(b => b.Json).Reduce((x, y) => x + "," + y)}}}").Reduce((x, y) => x + "," + y)}]"
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static string ToJson(IEnumerable<IEnumerable<AttributeInfo>> attributes)
            => $"[{attributes.Select(a => $"{{{a.Select(b => b.Json).Reduce((x, y) => x + "," + y)}}}").Reduce((x, y) => x + "," + y)}]";

        /// <summary>
        /// JSON Object: $"{{{attribute.Json}}}"
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string ToJson(AttributeInfo attribute) => $"{{{attribute.Json}}}";
    }
}
