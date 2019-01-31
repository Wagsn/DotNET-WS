using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Shell
{
    /// <summary>
    /// 可执行的
    /// </summary>
    public interface IExecutable
    {
        IResult Execute(IArguments arguments);
    }
}
