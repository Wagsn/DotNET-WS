#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.CmdUnit
* 项目描述 ：
* 类 名 称 ：AddCmd
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.CmdUnit
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/22 10:38:40
* 更新时间 ：2018/11/22 10:38:40
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.CmdUnit
{
    /// <summary>
    /// 不定个数的32位数字加法运算
    /// </summary>
    public class AddCmd : CmdUnitBase
    {
        // 表达式加法运算
        // 上下文用于对变量进行加法运算
        public AddCmd(ShellContext context) : base(context) { }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            // TODO 添加环境变量支持
            if (string.IsNullOrWhiteSpace(arg))
            {
                Console.WriteLine(0);
                return 0;
            }
            else
            {
                string[] numRaws = arg.Trim().Split(' ');  // 这里切割之前需要过滤空格
                List<int> nums = new List<int>();
                foreach (var numRaw in numRaws)
                {
                    // numRaw 可能为空格字符串，这是因为前面没有过滤
                    if (string.IsNullOrWhiteSpace(numRaw))
                    {
                        continue;
                    }
                    try
                    {
                        int tmp = int.Parse(numRaw);
                        nums.Add(tmp);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("数字解析失败: \r\n" + e);
                        return 1;
                    }

                }
                int result = 0;
                foreach (int num in nums)
                {
                    result += num;
                }
                Console.WriteLine("< " + result);
                return 0;
            }
        }

        public override void Init()
        {
            Name = "add";
            Desc = "对一串数字进行加法运算，Int32";
            Usage = "add [11 [561 [...]]]";
        }
    }
}
