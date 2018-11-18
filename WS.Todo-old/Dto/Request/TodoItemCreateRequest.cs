using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WS.Todo.Dto
{
    public class TodoItemCreateRequest
    {
        /// <summary>
        /// 登入系统的用户ID
        /// </summary>
        [Required(ErrorMessage ="用户ID不能为空")]
        public long UserId { get; set; }
        
        /// <summary>
        /// 待办名
        /// </summary>
        [MaxLength(127)]
        public string Name { get; set; }
        
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
