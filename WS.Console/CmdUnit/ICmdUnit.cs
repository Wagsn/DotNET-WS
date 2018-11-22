using System;
using System.Collections.Generic;
using System.Text;

using WS.Core.Helpers;

namespace WS.Shell.CmdUnit
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

        /// <summary>
        /// 初始化
        /// </summary>
        void Init();

        /// <summary>
        /// 上下文初始化
        /// </summary>
        /// <param name="context"></param>
        void Init(ShellContext context);

        /// <summary>
        /// 命令执行，返回0执行成功，其它为失败
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        int Excute(string arg);
    }
}
