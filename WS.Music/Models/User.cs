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
        [MaxLength(36, ErrorMessage = "GUID最长不超过36")]
        public string Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(31)]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(63)]
        public string Pwd { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [MaxLength(255)]
        public string Mail { get; set; }

        /// <summary>
        /// 介绍，可空（Empty=Blank>Null）
        /// </summary>
        [MaxLength(511)]
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
