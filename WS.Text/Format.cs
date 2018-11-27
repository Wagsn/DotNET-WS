#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Text
* 项目描述 ：
* 类 名 称 ：Format
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Core.Text
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 1:27:26
* 更新时间 ：2018/11/22 1:27:26
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WS.Text
{
    /// <summary>
    /// 字符串格式化
    /// </summary>
    public static class Format
    {
        /// <summary>
        /// 去除首尾空格，再合并连续空格
        /// </summary>
        /// <param name="src">原始字符串</param>
        /// <returns></returns>
        public static string NormalSpace(string src)
        {
            return Regex.Replace(src.Trim(), "\\s{2,}", " ");
        }

        /// <summary>
        /// 使文件路径标准化（首先剔除掉文件路径不能包含的特殊字符）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string NormalPath(string path)
        {
            // 剔除掉不能存在的特殊字符
            // 归并掉相对路径 （"/p1/./p2/p3/../p4" -> "/p1/p2/p4"）
            return "";
        }

        /// <summary>
        /// 是否是路径
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsPath(string src)
        {

            return false;
        }

        /// <summary>
        /// 测试模板替换，vs2017->鼠标方法名->右键交互执行->需要把那些依赖的方法和using都交互执行过才行
        /// </summary>
        public static void Test()
        {
            SafeMap<object> map = new SafeMap<object>();
            map["username"] = "wagsn";
            map["password"] = "123456";

            Console.WriteLine(ReplacePlaceholder("${username}: ${password}", map));
        }


        /// <summary>
        /// 占位符替换: ${}
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string ReplacePlaceholder(string template, SafeMap<object> pairs)
        {
            string result = new string(template.ToCharArray());

            // 需要优化为，匹配到 \$\{\S*?\} 后按照匹配到的内容作为Key在Map中寻找Value替换
            foreach(var key in pairs.Keys)
            {
                Regex regex = new Regex(@"\$\{"+key+@"\}");
                result =regex.Replace(result, pairs[key].ToString());

            }
            return result;
        }

        /// <summary>
        /// 会自动替换 变量   把形如 "{{varName}}" 替换成对应的数值
        /// </summary>
        private static string ReplaceStringVar(string str)
        {
            Regex reg = new Regex(@"\{\{(.*?)\}\}");
            //var mat = reg.Matches(webcofnigstring2);

            str = reg.Replace(str,
                new MatchEvaluator(m =>
                     InstallContext.Get(m.Groups[1].Value) == string.Empty ? m.Value : InstallContext.Get(m.Groups[1].Value)
                ));
            return str;
        }
    }

    /// <summary>
    /// 安全的映射表，不存在的Key返回默认的Value，string返回string.Empty
    /// </summary>
    public class SafeMap<TValue>
    {
        /// <summary>
        /// 获取Keys
        /// </summary>
        public IEnumerable<string> Keys { get
            {
                return kvs.Keys;
            } }

        private Dictionary<string, TValue> kvs = new Dictionary<string, TValue>();
        
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TValue this[string index]
        {
            get
            {
                return Get(index);
            }
            set
            {
                Set(index, value);
            }
        }

        /// <summary>
        /// 获取Key对应的值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TValue Get(string index)
        {
            if (kvs.ContainsKey(index))
            {
                return kvs[index];
            }
            else
            {
                return default(TValue);
            }
        }
        /// <summary>
        /// 设置Key对应的值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Set(string index, TValue value)
        {
            kvs[index] = value;
        }
    }

    /// <summary>
    /// 键值对
    /// </summary>
    public static class InstallContext
    {

        private static Dictionary<string, string> kvs = new Dictionary<string, string>();


        public static string Get(string index)
        {
        kvs.Keys
            if (kvs.ContainsKey(index))
            {
                return kvs[index];
            }
            else
            {
                return string.Empty;
            }
        }
        public static void Set(string index, string value)
        {
            kvs[index] = value;
        }



        //private static InstallContext instance = new InstallContext();

        //private InstallContext()
        //{

        //}

        //public static InstallContext GetInstance()
        //{ 
        //    return instance;
        //}

    }
}

