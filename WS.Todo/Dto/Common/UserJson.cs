using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Dto
{
    /// <summary>
    /// 用户核心信息
    /// </summary>
    public class UserJson
    {
        [MaxLength(36, ErrorMessage =Define.Constants.IdLengthErrMsg)]
        public string Id;

        [MaxLength(31)]
        [MinLength(3, ErrorMessage ="用户名字符数不能低于3")]
        public string Name;

        [MaxLength(63)]
        [MinLength(8, ErrorMessage ="密码字符数不能低于8")]  // 添加密码验证正则表达式特性
        public string Pwd;
    }
}
