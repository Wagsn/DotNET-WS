using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WS.Text;

namespace WS.Shell
{
    /// <summary>
    /// 打印对象
    /// </summary>
    public class PrintData : VarData
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override VarData Run(VarData[] args)
        {
            var datas = args.Select(vd => vd.Get().Data);
            // 这里需要重新解析一下
            var show = JsonUtil.ToJson(datas);
            Console.WriteLine(show);
            return new NoneData();
        }
    }
}
