#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.CmdUnit
* 项目描述 ：
* 类 名 称 ：HelpCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 10:38:02
* 更新时间 ：2018/11/22 10:38:02
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// Help命令
    /// </summary>
    public class HelpCmd : CmdUnitBase
    {
        public HelpCmd(ShellContext context) : base(context) { }

        public override void Init()
        {
            Name = "help";
            Desc = "显示帮助信息";
            Usage = "help";  // TODO help <cmd.Name> // 显示某个命令的帮助信息
        }

        /// <summary>
        /// 显示各种命令，需要动态计算每个元素的长度，控制台或文本文件输出表格
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            // [command] 9, [usage] 7, [decription] 12
            // maxLenOfName
            // maxLenOfUsage
            // maxLenOfDesc
            // 构建二维字串矩阵
            int rowNum = AppContext.cmdManager.Count;
            string[][] strMat2D = new string[rowNum + 1][];
            // 表头
            strMat2D[0] = new string[] { "[command]：命令", "[usage]：用法", "[decription]：描述" };
            // 表体
            int i = 1;
            foreach (var cmdpairs in AppContext.cmdManager.CmdMap)
            {
                var cmd = cmdpairs.Value;
                strMat2D[i] = new string[3];
                strMat2D[i][0] = cmd.Name;
                strMat2D[i][1] = cmd.Usage;
                strMat2D[i][2] = cmd.Desc;
                i++;
            }
            string grid = WS.Text.Grid.ToGrid(strMat2D);
            Console.WriteLine(grid);
            WS.IO.File.WriteAllText("./grid/" + DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid() + ".txt", grid);
            //File.WriteAllText("./grid/" + DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid() + ".txt", );
            return 0;
        }

        // 在这里写制表函数，输入二维字符串数组，输出表格

        /// <summary>
        /// 获取字符串的宽度
        /// 输入：不含制表符之类的字符串
        /// 处理：中文占2半角宽度，英文数字占1半角宽度
        /// 输出：字符串在控制台显示的宽度
        /// </summary>
        /// <param name="str">不含制表符的字符串</param>
        /// <returns></returns>
        private int WidthOf(string str)
        {
            // 不考虑制表符且字符集为GBK System.Text.Encoding.GetEncoding("GBK").GetByteCount(str);
            return DisplayLength(str);
        }

        /// <summary>
        /// 显示长度，考虑制表符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int DisplayLength(string str)
        {
            int lengthCount = 0;
            var splits = str.ToCharArray();
            for (int i = 0; i < splits.Length; i++)
            {
                if (splits[i] == '\t')
                {
                    lengthCount += 8 - lengthCount % 8;
                }
                else
                {
                    lengthCount += Encoding.Unicode.GetByteCount(splits[i].ToString());
                }
            }
            return lengthCount;
        }

        private string SpaceFor(int len)
        {
            return "";
        }
    }
}
