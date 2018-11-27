using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Models
{
    /// <summary>
    /// 待办项历史变更表
    /// </summary>
    public class TodoItemHistory
    {
        /// <summary>
        /// 变更历史ID
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }
        
        /// <summary>
        /// 变更用户GUID
        /// </summary>
        [MaxLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 变更类型（Read：这个不需要，Create，Update，Delete）
        /// </summary>
        [MaxLength(15)]
        public string Type { get; set; }

        /// <summary>
        /// 变更内容（原始待办的JSON字符串）
        /// </summary>
        [MaxLength(1023)]
        public string Content { get; set; }

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime? Time { get; set; }

    }
}
