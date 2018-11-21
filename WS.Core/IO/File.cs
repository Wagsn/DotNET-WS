#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Core.IO
* 项目描述 ：
* 类 名 称 ：File
* 类 描 述 ：文件工具
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Core.IO
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/21 22:17:06
* 更新时间 ：2018/11/21 22:17:06
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WS.Core.IO
{
    /// <summary>
    /// 文件
    /// </summary>
    public class File
    {
        /// <summary>
        /// 文件写入内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void WriteAllText(string path, string contents)
        {
            // File.ReadAllText(FilePath) //File.ReadAllText(FilePath, Encoding)
            // File.WriteAllText(@"c:\temp\test\ascii.txt", str1);
            // "./grid/" + DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid() + ".txt"
            if (System.IO.File.Exists(path))
            {
                System.IO.File.WriteAllText(path, contents);
            }
            else
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                    System.IO.File.CreateText(path);
                }
                System.IO.File.WriteAllText(path, contents);
            }
        }
    }
}
