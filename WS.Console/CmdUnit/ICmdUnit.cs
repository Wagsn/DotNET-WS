﻿using System;
using System.Collections.Generic;
using System.Text;

using WS.Core.Helpers;

namespace WS.Shell
{
    /// <summary>
    /// 命令通用接口(最小单位)
    /// </summary>
    public interface ICmdUnit
    {
        /// <summary>
        /// 命令描述(Description)
        /// </summary>
        string Desc { get; set; }

        /// <summary>
        /// 命令名：tosjon
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 命令使用方法描述(use-method)：tojson [argument] 格式参考[/doc/公约.md#命令调用方法]
        /// </summary>
        string Usage { get; set; }

        /// <summary>
        /// 应用上下文
        /// </summary>
        ShellContext AppContext { get; set; }

        void Init();

        void Init(ShellContext context);

        /// <summary>
        /// 命令执行，返回0执行成功，其它为失败
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        int Excute(string arg);
    }

    /// <summary>
    /// 命令单元的基类
    /// </summary>
    public abstract class CmdUnitBase : ICmdUnit
    {
        public string Desc { get; set; }
        public string Name { get; set; }
        public string Usage { get; set; }
        public ShellContext AppContext { get; set; }

        // 实现
        public abstract void Init();

        // 实现
        public abstract int Excute(string arg);

        // 初始化上下文，有些可能不需要上下文
        public void Init(ShellContext context)
        {
            AppContext = context;
            Init();
        }

        public CmdUnitBase()
        {
            Init();
        }

        public CmdUnitBase(ShellContext context)
        {
            Init(context);
        }
    }

    /// <summary>
    /// Help命令
    /// </summary>
    public class HelpCmd : CmdUnitBase
    {
        public HelpCmd(): base() { }

        public HelpCmd(ShellContext context):base(context) { }

        public override void Init()
        {
            Name = "help";
            Desc = "显示帮助信息";
            Usage = "help";  // TODO help <cmd.Name> // 显示某个命令的帮助信息
        }

        /// <summary>
        /// 显示各种命令，需要动态计算每个元素的长度
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            // [command] 9, [usage] 7, [decription] 12
            // maxLenOfName
            // maxLenOfUsage
            // maxLenOfDesc
            Console.WriteLine("[command]\t[usage]\t\t\t\t[decription]");
            foreach (var cmdpairs in AppContext.CmdMap)
            {
                var cmd = cmdpairs.Value;
                Console.WriteLine(cmd.Name + "\t\t" + cmd.Usage + "\t\t" + cmd.Desc);
            }
            return 0;
        }

        private string SpaceFor(int len)
        {
            return "";
        }
    }

    /// <summary>
    /// 查看文本文件，将文本输出到控制台
    /// </summary>
    public class ViewCmd : CmdUnitBase
    {
        public ViewCmd(): base() { }
        public ViewCmd(ShellContext context) : base(context) { }

        public override void Init()
        {
            Name = "view";
            Desc = "在控制台上显示文本文件的内容";
            Usage = "view <file path>";
        }

        public override int Excute(string arg)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// ToJson命令
    /// </summary>
    public class ToJsonCmd : CmdUnitBase
    {
        public ToJsonCmd() : base() { }
        public ToJsonCmd(ShellContext context): base(context) {}

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            Name = "tojson";
            Desc = "将对象转换成字符串、暂时功能不全。";  // tojson需要将argument参数解析成变量组，在变量表中寻找变量的实际值，然后传递给tojson函数，这里需要将tojson封装成一个函数。
            Usage = "tojson [argument]";
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            // 解析arg为参数组，在context的变量表中找出这些变量，再将变量转换成JSON字符串输出的到控制台
            Console.WriteLine(JsonHelper.ToJson(arg) + "\r\n");
            return 0;
        }
    }

    /// <summary>
    /// 现在的时间
    /// </summary>
    public class NowCmd : CmdUnitBase
    {
        public NowCmd() : base() { }

        public override void Init()
        {
            Name = "now";
            Desc = "显示当前时间";
            Usage = "now";  // TODO now <format>  // now "yy-MM-dd HH:mm:ss.SSS"
        }

        public override int Excute(string arg)
        {
            Console.WriteLine(DateTime.Now.ToString(Define.Format.Time));
            return 0;
        }
    }

    /// <summary>
    /// 变量声明
    /// </summary>
    public class VarCmd : CmdUnitBase
    {
        public VarCmd(ShellContext context) : base(context) { }

        public override void Init()
        {
            Name = "var";
            Desc = "变量声明";
            Usage = "var num:Int32=32141";
        }

        /// <summary>
        /// 执行
        /// 参数; "num:Int32=32141"  // 单变量定义
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            // 单变量定义
            // 正则验证
            // 切割
            string[] vars = arg.Split("=");
            // 声明部分
            string declare = vars[0];
            // 值部分 字符串形式
            string valueRaw = vars[1];
            return -1;
        }
    }

    /// <summary>
    /// 不定个数的32位数字加法运算
    /// </summary>
    public class AddCmd : CmdUnitBase
    {
        // 表达式加法运算
        public AddCmd() : base() { }
        // 上下文用于对变量进行加法运算
        public AddCmd(ShellContext context) : base(context) { }

        public override int Excute(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                Console.WriteLine(0);
                return 0;
            }
            else
            {
                string[] numRaws = arg.Trim().Split(" ");  // 这里切割之前需要过滤空格
                List<int> nums = new List<int>();
                foreach(var numRaw in numRaws)
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
                    catch(Exception e)
                    {
                        Console.WriteLine("数字解析失败: \r\n"+e);
                        return 1;
                    }
                    
                }
                int result = 0;
                foreach(int num in nums)
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

    /// <summary>
    /// 控制台输出
    /// </summary>
    public class EchoCmd : CmdUnitBase
    {
        /// <summary>
        /// 用于输出变量集中的数据
        /// </summary>
        /// <param name="context"></param>
        public EchoCmd(ShellContext context): base(context) { }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override int Excute(string arg)
        {
            Console.WriteLine(arg);
            return 0;
        }

        public override void Init()
        {
            Name = "echo";
            Desc = "输出到控制台";
            Usage = "ccho [string]";
        }
    }

    /// <summary>
    /// 清除控制台的显示
    /// </summary>
    public class ClearCmd : CmdUnitBase
    {
        public ClearCmd(ShellContext context): base(context) { }

        public override int Excute(string arg)
        {
            Console.Clear();
            return 0;
        }

        public override void Init()
        {
            Name = "clear";
            Desc = "清除控制台显示";
            Usage = "clear";
        }
    }

    /// <summary>
    /// 显示当前启动ID
    /// </summary>
    public class IdCmd : CmdUnitBase
    {
        public IdCmd(ShellContext context): base(context) { }

        public override int Excute(string arg)
        {
            Console.WriteLine(AppContext.StartId);
            return 0;
        }

        public override void Init()
        {
            Name = "id";
            Desc = "显示当前启动ID";
            Usage = "id";
        }
    }
}
