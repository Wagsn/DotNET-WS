using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionCenter
{
    /// <summary>
    /// 简单富文本描述
    /// </summary>
    public class SimpleRichText
    {
        /// <summary>
        /// 富文本主体
        /// </summary>
        public List<RToken> Body { get; } = new List<RToken>();
    }

    /// <summary>
    /// 节点
    /// </summary>
    public class RToken
    {
        /// <summary>
        /// 名称 (如: UserName, Email, Phone)
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 标签 div span
        /// </summary>
        public string Label { get; set; } = "span";

        /// <summary>
        /// 值 (如: "Wagsn", "wagsn@foxmail.com", "12345678901")
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// 属性
        /// </summary>
        public List<Attribute> Attributes { get; } = new List<Attribute>();

        /// <summary>
        /// 样式
        /// </summary>
        public RStyle Style { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<RToken> Childs { get; } = new List<RToken>();

        /// <summary>
        /// 输出HTML片段
        /// </summary>
        /// <returns></returns>
        public string ToHtml()
        {
            var styleAttr = new List<Attribute>();

            var styleAttr2 = new List<Attribute>
            {
                new Attribute("color", $"{Style.Color}"),
                new Attribute("background-color", $"{Style.BgColor}"),
            };
            styleAttr2.RemoveAll(a => Style.ExtraStyles.Any(b => b.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture) == a.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture)));
            styleAttr2.AddRange(Style.ExtraStyles);
            styleAttr.AddRange(styleAttr2);

            //var dic = new Dictionary<string, string>()
            //{
            //    { Name = "color", Value = $"{Style.Color}" },
            //    { Name = "background-color", Value = $"{Style.BgColor}" },
            //};
            //foreach(var item in Style.ExtraStyles)
            //{
            //    dic[item.Name] = item.Value;
            //}
            //styleAttr = dic.Select(kv => new Attribute(kv.Key, kv.Value)).ToList();

            // Comparison
            styleAttr.AddRange(styleAttr2.Union(Style.ExtraStyles, new AttributeNameComparer()));

            Attributes.RemoveAll(a => a.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture) == "style");
            var style = $" style=\" {string.Join(";", styleAttr.Select(a => $"{a.Name}:{a.Value}"))}\"";
            var attr = Attributes.Count > 0 ? $" {string.Join(" ", Attributes.Select(a => $"{a.Name}=\"{a.Value}\""))}{style}" : $"{style}";
            return $"<{Label}{attr}>{Value}{string.Join("", Childs.Select(t => t.ToHtml()))}</{Label}>";
        }
    }

    /// <summary>
    /// 属性
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// 名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        public Attribute() { }
        public Attribute(string name, string value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }

    public class AttributeNameComparer : IEqualityComparer<Attribute>
    {
        public bool Equals([AllowNull] Attribute x, [AllowNull] Attribute y)
        {
            if(x == null && y == null)
            {
                return true;
            }
            if(x == null || y == null)
            {
                return false;
            }
            return x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Attribute obj)
        {
            return 0;
        }
    }

    /// <summary>
    /// 样式
    /// </summary>
    public class RStyle
    {
        /// <summary>
        /// 前景颜色
        /// </summary>
        public Color Color { get; set; } = Color.Black;

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color BgColor { get; set; } = Color.White;

        /// <summary>
        /// 额外样式（额外样式包含主样式，则额外样式生效）
        /// </summary>
        public List<Attribute> ExtraStyles { get; } = new List<Attribute>();
    }
}
