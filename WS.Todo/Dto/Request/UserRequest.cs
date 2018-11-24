using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Dto
{
    /// <summary>
    /// 用户请求
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        [Required]
        public UserJson User { get; set; }
    }
}
