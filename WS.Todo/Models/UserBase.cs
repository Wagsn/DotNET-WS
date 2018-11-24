using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserBase : TraceUpdate
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [MaxLength(36, ErrorMessage ="Id的字符数不能超过36")]
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(31, ErrorMessage ="用户名字符数不能超过31")]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(63, ErrorMessage ="密码字符数不能超过63")]
        public string Pwd { get; set; }
    }
}
