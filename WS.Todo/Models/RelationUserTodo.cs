using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Models
{
    /// <summary>
    /// 用户和待办的关联表
    /// </summary>
    public class RelationUserTodo
    {
        [MaxLength(length: 36, ErrorMessage = Define.Constants.IdLengthErrMsg)]
        public string UserId { get; set; }

        public string TodoId { get; set; }
    }
}
