using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell.Interpreter
{
    /// <summary>
    /// 主函数
    /// </summary>
    public class FunctionMain : VarData
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override VarData Run(VarData[] args)
        {
            //VarTable
            return base.Run(args);
        }
    }
}
