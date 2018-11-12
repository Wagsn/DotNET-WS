using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Dto
{
    /// <summary>
    /// 携带消息响应体（用于返回错误信息）
    /// </summary>
    public class ResponseMessage
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; }

        public ResponseMessage()
        {
            Code = ResponseCodeDefines.SuccessCode;
        }

        public bool IsSuccess()
        {
            if (Code == ResponseCodeDefines.SuccessCode)
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
