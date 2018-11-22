using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Core.Log
{
    public class LogConfig
    {
        public LogConfig() { }

        public string LogFileTemplate { get; set; }
        public string LogContentTemplate { get; set; }
        public bool IsAsync { get; set; }
        public string LogBaseDir { get; set; }
        public LogLevels LogLevels { get; set; }
        public int MaxArchiveFiles { get; set; }
        public string MaxFileSize { get; set; }
        public string ArchiveFileTemplate { get; set; }
        public string ArchiveBaseDir { get; set; }
        public string ArchiveWriteMode { get; set; }
        public string ArchiveEvery { get; set; }
        public bool? ZipArchiveFile { get; set; }
    }
}
