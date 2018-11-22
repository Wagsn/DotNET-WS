#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Shell.Plugin
* 项目描述 ：
* 类 名 称 ：Plugins
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Shell.Plugin
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/20 17:47:20
* 更新时间 ：2018/11/20 17:47:20
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.Plugin
{
    using System.Threading.Tasks;

    /// <summary>
    /// 插件配置接口
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public interface IPluginConfig<TConfig>
    {
        Type ConfigType { get; }

        Task<PluginMessage> ConfigChanged(ShellContext context, TConfig newConfig);
        Task<TConfig> GetConfig(ShellContext context);
        TConfig GetDefaultConfig(ShellContext context);
        Task<bool> SaveConfig(TConfig cfg);
    }

    public class PluginMessage
    {
        public PluginMessage() { }

        public string Code { get; set; }
        public string Message { get; set; }

        public bool IsSuccess() { return false; }
    }
}
