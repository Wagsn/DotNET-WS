using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 脚本上下文
    /// </summary>
    public class ScriptContext
    {
        /// <summary>
        /// 变量表
        /// </summary>
        public IList<VarEntry> VarTable { get; set; }

        public ScriptContext()
        {
            VarTable = new List<VarEntry>();
        }
    }
}
