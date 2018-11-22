using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core.Dto;
using WS.Core.Text;

namespace WS.Music.Controllers
{
    /// <summary>
    /// 通用控制器基类，提供一些通用方法模板
    /// </summary>
    public class WSControllerBase : ControllerBase
    {
        /// <summary>
        /// 通用Get方法，Query Id
        /// </summary>
        /// <typeparam name="Ext"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseMessage<Ext> Get<Ext>([FromQuery]long Id)
        {
            // 日志输出：请求体
            Console.WriteLine("WS------ Request: \r\n" + "Id: " + Id);
            // 创建响应体
            ResponseMessage<Ext> response = new ResponseMessage<Ext>();
            // 模型验证
            if (!Util.ModelValidCheck(ModelState, response))
            {
                return response;
            }
            try
            {
                // 业务处理，调用实际处理函数，TODO：改成委托
                response.Extension = HandleQueryId<Ext>(Id);
            }
            catch (Exception e)
            {
                response.Code = ResponseDefine.ServiceError;
                response.Message += "\r\n" + e.Message;
                // 日志输出：服务器错误
                Console.WriteLine("WS------ ServiceError: \r\n" + e);
            }
            // 判断是否查询到数据
            if (response.Code == "0" && response.Extension == null)
            {
                response.Code = ResponseDefine.NotFound;
                // 日志输出：找不到资源
                Console.WriteLine("WS------ NotFund: \r\n" + "");
            }
            // 日志输出：响应体
            Console.WriteLine("WS------ Response: \r\n" + JsonHelper.ToJson(response));
            return response;
        }

        /// <summary>
        /// 实际处理函数
        /// </summary>
        /// <typeparam name="TReult"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected virtual TReult HandleQueryId<TReult>(long Id)
        {
            throw new NotSupportedException();
        }
    }
}
