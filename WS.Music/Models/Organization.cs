using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Models
{
    /// <summary>
    /// 组织，用于艺人团体管理，注：暂时不用
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? BuildTime { get; set; }
    }
}
