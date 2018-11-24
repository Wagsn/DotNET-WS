using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Dto
{
    /// <summary>
    /// 待办项，前端显示数据，为了不暴露数据库追踪字段
    /// </summary>
    public class TodoItemJson
    {
        /// <summary>
        /// 待办ID
        /// </summary>
        [Key]
        [MaxLength(36, ErrorMessage = "待办ID不能超过36个字符")]
        public string Id { get; set; }

        /// <summary>
        /// 待办名
        /// </summary>
        [MaxLength(31, ErrorMessage = "待办名不能超过31个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 待办内容
        /// </summary>
        [MaxLength(255, ErrorMessage = "待办内容不能超过255个字符")]
        public string Content { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        [Required]
        public bool IsComplete { get; set; }

        /// <summary>
        /// 预期完成时间
        /// </summary>
        public DateTime? ExpectTime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? ActualTime { get; set; }
    }
}
