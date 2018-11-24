using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WS.Todo.Dto
{
    /// <summary>
    /// 待办项[查询|创建|更新|删除]请求
    /// </summary>
    public class TodoItemRequest :UserRequest
    {
        /// <summary>
        /// 新建的待办项
        /// </summary>
        [Required]
        public TodoItemJson Todo { get; set; }
    }
}
