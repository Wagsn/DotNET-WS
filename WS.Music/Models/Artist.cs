using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using WS.Core.Models;

namespace WS.Music.Models
{
    /// <summary>
    /// 艺人，艺人与用户是分离的
    /// </summary>
    public class Artist : TraceUpdateBase
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? BirthTime { get; set; }

        public Artist() { }

        public Artist(Artist a)
        {
            Id = a.Id;
            Name = a.Name;
            Description = a.Description;
            BirthTime = a.BirthTime;
        }
    }
}
