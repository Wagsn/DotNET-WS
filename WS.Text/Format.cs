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
        /// 占位符替换: ${}
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string ReplacePlaceholder(string template, Dictionary<string, object> pairs)
        {
            // 截取索引及其占位符
            //Regex regex = new Regex(@"\$\{\S*\}");
            //Match match = regex.Match(template);
            //string value = match.Value;
            // ${username}
            // 替换
            return "";
        }
    }
}

