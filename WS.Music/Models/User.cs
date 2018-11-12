using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Models;

namespace WS.Music.Models
{
    /// <summary>
    /// 用户，艺人与用户是分离的
    /// </summary>
    public class User : TraceUpdateBase
    {
        /// <summary>
        /// ID，主键
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 介绍，可空（Empty=Blank>Null）
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 性别，可空（Null：未知，true：男，false：女）
        /// </summary>
        public bool? Sex { get; set; }
        
        /// <summary>
        /// 生日，可空
        /// </summary>
        public DateTime? BirthTime { get; set; }

        // Fans:List<User> Follows:List<User> Event
    }
}
