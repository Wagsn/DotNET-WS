﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：WS.Core.Dto
* 项目描述 ：
* 类 名 称 ：ResponseMessage
* 类 描 述 ：
* 所在的域 ：DESKTOP-KA4M82K
* 命名空间 ：WS.Core.Dto
* 机器名称 ：DESKTOP-KA4M82K 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：wagsn
* 创建时间 ：2018/11/7 17:44:33
* 更新时间 ：2018/11/7 17:44:33
* 版 本 号 ：v1.0.0.0
//----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.Dto
{
    /// <summary>
    /// 携带消息响应体（用于返回错误信息）
    /// </summary>
    public class ResponseMessage
    {
        private string code;

        /// <summary>
        /// 响应码
        /// </summary>
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                switch (value)
                {
                    case "0":
                        Message = ResponseDefine.SuccessMsg;
                        break;
                    case "100":
                        Message = ResponseDefine.ModelStateInvalidMsg;
                        break;
                    case "101":
                        Message = ResponseDefine.ArgumentNullErrorMsg;
                        break;
                    case "400":
                        Message = ResponseDefine.BadRequsetMsg;
                        break;
                    case "403":
                        Message = ResponseDefine.NotAllowMsg;
                        break;
                    case "404":
                        Message = ResponseDefine.NotFoundMsg;
                        break;
                    case "500":
                        Message = ResponseDefine.ServiceErrorMsg;
                        break;
                    case "501":
                        Message = ResponseDefine.NotSupportMsg;
                        break;
                    case "601":
                        Message = ResponseDefine.PostRepeatMsg;
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; }

        public ResponseMessage()
        {
            Code = ResponseDefine.SuccessCode;
            Message = ResponseDefine.SuccessMsg;
        }

        public bool IsSuccess()
        {
            if (Code == ResponseDefine.SuccessCode)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 携带数据的响应体（用于携带单个的数据）
    /// </summary>
    /// <typeparam name="TEx"></typeparam>
    public class ResponseMessage<TEx> : ResponseMessage
    {
        /// <summary>
        /// 携带的记录
        /// </summary>
        public TEx Extension { get; set; }
    }

    /// <summary>
    /// 分页查询的响应体（用于携带一组数据），组数前端自己计算（TotalCount/PageSize+1）
    /// 注：请求体的页码如果超限，则返回（第一页数据|索引错误）
    /// </summary>
    /// <typeparam name="Tentity"></typeparam>
    public class PagingResponseMessage<Tentity> : ResponseMessage<List<Tentity>>
    {
        /// <summary>
        /// 分页索引，当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页记录数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public long TotalCount { get; set; }
    }
}
