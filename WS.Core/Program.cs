using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WS.Core
{
    class Program
    {
        /// <summary>重庆中央公园
        /// 入口
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        static void Main(string[] args)
        {
            RunTest(TestAttributeInfo);
            //RunTest(TestTrimEmpty);
            Console.ReadKey();
        }

        static void TestAttributeInfo()
        {
            var attrs = new List<AttributeInfo>
            {
                new AttributeInfo
                {
                    Name = "Id", Value = "\"ab45-cd45-ef54\""
                },
                new AttributeInfo
                {
                    Name = "CreateTime", Value = "\"2018-10-15\""
                },
                new AttributeInfo
                {
                    Name = "UseDays", Value = "1542"
                },
                new AttributeInfo
                {
                    Name = "Sex", Value = "true"
                }
            };

            var attrs2 = new List<List<AttributeInfo>>
            {
                new List<AttributeInfo>
                {
                    new AttributeInfo
                    {
                        Name = "Id", Value = "\"ab45-cd45-ef54\""
                    },
                    new AttributeInfo
                    {
                        Name = "CreateTime", Value = "\"2018-10-15\""
                    },
                    new AttributeInfo
                    {
                        Name = "UseDays", Value = "1542"
                    },
                    new AttributeInfo
                    {
                        Name = "Sex", Value = "true"
                    }
                },
                new List<AttributeInfo>
                {
                    new AttributeInfo
                    {
                        Name = "Id", Value = "\"ab54-cd78-ef69\""
                    },
                    new AttributeInfo
                    {
                        Name = "CreateTime", Value = "\"2018-10-13\""
                    },
                    new AttributeInfo
                    {
                        Name = "UseDays", Value = "584"
                    },
                    new AttributeInfo
                    {
                        Name = "Sex", Value = "false"
                    }
                }
            };
            
            Console.WriteLine(AttributeInfo.ToJson(attrs));
            Console.WriteLine(AttributeInfo.ToJson(attrs2));
        }

        static void RunTest(Action action)
        {
            Console.WriteLine($"Test Unit {action.Method.Name}:\r\n");
            action();
        }
    }
}
