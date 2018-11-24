using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Todo.Dto
{
    /// <summary>
    /// 原子数据操作请求
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ModelRequest<TModel> : UserRequest
    {
        public TModel model { get; set; }
    }
}
