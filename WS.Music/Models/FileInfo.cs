using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Models
{
    /// <summary>
    /// 文件信息，注：暂时不用
    /// </summary>
    public class FileInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public string Url { get; set; }
    }
}
