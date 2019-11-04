using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionCenter.Dto
{
    /// <summary>
    /// 请求条件查询请求
    /// </summary>
    public class SearchConditionRequest
    {
        // SELECT * FROM subject s WHERE s.IsDelete = false AND (s.Email LIKE '%%' OR s.Phone LIKE '%%')
        /// <summary>
        /// 查询
        /// </summary>
        public List<MultiName> Selects { get; } = new List<MultiName>();

        /// <summary>
        /// 来自
        /// </summary>
        public List<MultiName> Froms { get; } = new List<MultiName>();

        /// <summary>
        /// (A OR B) AND (C OR D)
        /// GROUP OPERATOR STATEMENT
        /// </summary>
        public List<string> Wheres { get; } = new List<string>();

        /// <summary>
        /// Order By
        /// </summary>
        public List<Order> Orders { get; } = new List<Order>();

        public static void Test()
        {
            List<Dictionary<string, object>> pairs = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    ["name"] = "name",
                    ["email"] = "email",
                    ["phone"] = "phone",
                },
                new Dictionary<string, object>
                {
                    ["name"] = "name2",
                    ["email"] = "email2",
                    ["phone"] = "phone2",
                },
                new Dictionary<string, object>
                {
                    ["name"] = "name3",
                    ["email"] = "email3",
                    ["phone"] = "phone3",
                }
            };
            List<MultiName> multiNames = new List<MultiName>
            {
                new MultiName
                {
                    Name = "name",
                    Alias = "name_slias",
                },
                new MultiName
                {
                    Name = "email",
                    Alias = "email_slias",
                },
                new MultiName
                {
                    Name = "name",
                    Alias = "phone_slias",
                }
            };
            // Key值映射
            var output = pairs.Select(a => a.ToDictionary(ks => multiNames.Any(b => b.Name == ks.Key) ? ks.Key : multiNames.FirstOrDefault(b => b.Name == ks.Key).Alias, kv => kv.Value)).ToList();
        }
    }

    /// <summary>
    /// 复合名称（原名与别名）
    /// </summary>
    public class MultiName
    {
        /// <summary>
        /// 原名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
    }

    /// <summary>
    /// 查询条件
    /// </summary>
    public enum SearchType
    {
        GROUP = 1,
        OPERATOR = 2,
        STATEMENT = 3
    }

    public class Order
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 降序
        /// </summary>
        public bool Desc { get; set; }
    }
}
