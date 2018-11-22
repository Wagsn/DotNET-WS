using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Log
{
    public class LoggingEventArgs : EventArgs
    {
        public LoggingEventArgs(LogEntity logEntity) { }

        public LogEntity LogEntity { get; set; }
    }
}
