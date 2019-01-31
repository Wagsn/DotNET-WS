using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.Functions
{
    /// <summary>
    /// 函数通用接口(最小单位)
    /// </summary>
    public interface IFuncUnit
    {
        /// <summary>
        /// 函数描述(Description)
        /// </summary>
        string Desc { get; set; }

        /// <summary>
        /// 函数名：tosjon
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 函数使用方法描述(use-method)：tojson [argument] 格式参考[/doc/公约.md#命令调用方法]
        /// </summary>
        string Usage { get; set; }

        // 函数返回值与参数
    }
}
