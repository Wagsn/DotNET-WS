using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Workflow.Core
{
    /// <summary>
    /// 回调流程
    /// </summary>
    public class CallbackFlow
    {
        /// <summary>
        /// 流程ID
        /// </summary>
        public string FlowId { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// 下一个流程
        /// </summary>
        public string NextFlowId { get; set; }
    }
}
