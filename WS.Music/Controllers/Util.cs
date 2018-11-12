using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Dto;

namespace WS.Music.Controllers
{
    public static class Util
    {
        /// <summary>
        /// 模型验证，将会修改Response，为其添加错误码和错误信息，并输出日志
        /// </summary>
        /// <param name="modelState">模型状态字典</param>
        /// <param name="response">响应体</param>
        /// <returns></returns>
        public static bool ModelValidCheck(ModelStateDictionary modelState, ResponseMessage response)
        {
            if (!modelState.IsValid)
            {
                //如果模型验证失败，就返回失败代码和信息
                var error = "";
                var errors = modelState.Values.ToList();
                foreach (var item in errors)
                {
                    foreach (var e in item.Errors)
                    {
                        error += e.ErrorMessage.ToString();
                    }
                }
                response.Code = ResponseDefine.ModelStateInvalid;
                response.Message = error;
                // 日志输出：模型验证失败
                Console.WriteLine("WS------ ModelValid: \r\n" + error + "\r\n");
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
