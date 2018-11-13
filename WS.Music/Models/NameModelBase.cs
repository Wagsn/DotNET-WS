using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Models;

namespace Music.Models
{
    /// <summary>
    /// 有名实体，很多实体都具有ID与名称属性
    /// </summary>
    public class NameModelBase : TraceUpdateBase
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string Id { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
    }
}
