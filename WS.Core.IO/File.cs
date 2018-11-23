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
        /// 文件写入内容(是否追加：默认false，如果文件存在将被删除重建)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void WriteAllText(string path, string contents, bool append =false)
        {
            // 得到file，不存在则新建
            FileInfo textFile = new FileInfo(path);
            StreamWriter writer;
            if (!textFile.Exists)
            {
                DirectoryInfo textDir = textFile.Directory;
                if (!textDir.Exists)
                {
                    textDir.Create();
                }
                writer =textFile.CreateText();
            }
            else
            {
                if (!append)
                {
                    textFile.Delete();
                    writer =textFile.CreateText();
                }
                else
                {
                    writer = textFile.AppendText();
                }
            }
            writer.Write(contents);
            writer.Close();
        }
    }
}
