using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WS.NET.Extensions
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
            RunTest(TestReduce);
            //RunTest(TestTrimEmpty);
            Console.ReadKey();
        }

        static void RunTest(Action action)
        {
            Console.WriteLine($"Test Unit {action.Method.Name}:\r\n");
            action();
        }
        class Person
        {
            public string Name { get; set; }
        }
        static void TestReduce()
        {
            //var ps = new List<Person> { new Person { Name = "123" }, new Person { Name = "456" }, new Person { Name = "123" } };
            //ps.GroupBy(a => a.Name).Select(g => g.FirstOrDefault());
            //Console.WriteLine(string.Join(",", new List<Person> { new Person { Name = "123" }, new Person { Name = "456" }, new Person { Name = "123" } }.Select(s => s.Name)));
            //Console.WriteLine(ps.Reduce((x, y) => new Person { Name = x.Name +","+ y.Name }).Name);
            //Console.WriteLine(string.Join(",", ps.GroupBy(a => a.Name).Select(g => g.FirstOrDefault()).Select(a => a.Name)));
            //Console.WriteLine(ps.GroupBy(a => a.Name).Select(g => g.FirstOrDefault()).Select(a => a.Name).Reduce((x, y) => x + "," + y));


            string str = "123456789";  // 48-57
            //Console.WriteLine("0 ascii: " + Convert.ToInt32('0'));  // 48
            //Console.WriteLine("0 ascii: " + Convert.ToInt32('9'));  // 57
            Console.WriteLine("123456789 convert to integer: " + str.Select(c => c - 48).Reduce((x, y) => x * 10 + y));


            //var strs = new List<string> { "123", "456", "123", "789" };
            //Console.WriteLine(string.Join(",", strs));
            //Console.WriteLine(strs.Sum(Convert.ToInt32));
            //Console.WriteLine(strs.Reduce((a, b) => a + b));
            //Console.WriteLine(strs.Select(s => Convert.ToInt32(s)).Reduce((a, b) => a * 1000 + b));
        }

        static void TestTrimEmpty()
        {
            var pers = new List<Person> { new Person { Name = "123" }, new Person { Name = "456" }, new Person { Name = "123" } };
            Console.WriteLine(pers.Select(s => s.Name).Reduce((x, y) => x + "," + y));
            Console.WriteLine(string.Join(",", pers.Distinct((x, y) => x.Name == y.Name).Select(s => s.Name)));

            var ints = new List<string> { "123", "   ", null };
            Console.WriteLine(ints.Select(a => string.IsNullOrWhiteSpace(a) ? "IsNullOrWhiteSpace" : a).Reduce((x, y) => x + "," + y));
            Console.WriteLine(ints.TrimEmpty().Select(a => string.IsNullOrWhiteSpace(a) ? "IsNullOrWhiteSpace" : a).Reduce((x, y) => x + "," + y));
            Console.WriteLine(ints.Select(a => a == null ? "IsNull" : a.ToString()).Reduce((x, y) => x + "," + y));
            Console.WriteLine("TrimEmpty: " + ints.TrimEmpty().Select(a => a == null ? "IsNull" : a.ToString()).Reduce((x, y) => x + "," + y));
            Console.WriteLine("Filter: " + ints.Filter(a => a == null).Select(a => a == null ? "IsNull" : a.ToString()).Reduce((x, y) => x + "," + y));
        }
    }
}
