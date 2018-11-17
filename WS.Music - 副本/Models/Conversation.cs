using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Models
{
    /// <summary>
    /// 会话（?群组有管理关系）实体，两个及以上的人参与，邀请新的人参与将产生新的会话
    /// </summary>
    public class Conversation
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
        public string Name { get; set; }

        /// <summary>
        /// 参与人
        /// </summary>
        public List<User> UserList { get; set; }
    }
}
