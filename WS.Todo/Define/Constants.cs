using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Define
{
    public static class Constants
    {
        /// <summary>
        /// Id的字符数不能超过36 const静态常量（编译期决定值） readonly动态常量
        /// </summary>
        public const string IdLengthErrMsg = "Id的字符数不能超过36";
    }
}
