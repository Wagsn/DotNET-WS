using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using WS.Core.Dto;
using WS.Core.Helpers;

/// <summary>
/// 项目专用的总常量池，TODO：将所有常量迁移到这里来，不过这样会导致其它地方不好创建常量，建议一个模块一个常量池类，其他地方的常量也可以采用这种命名空间
/// </summary>
namespace WS.Music.Def
{
    /// <summary>
    /// 用户常量
    /// </summary>
    public static class User
    {
        public static readonly string TAG = "User";
        /// <summary>
        /// 找不到用户，可能是未注册或已注销，请你检查一下你的请求是否正确
        /// </summary>
        public static readonly string NotFoundMsg = "找不到用户，可能是未注册或已注销，请你检查一下你的请求是否正确";


    }

    /// <summary>
    /// 歌曲常量
    /// </summary>
    public static class Song
    {
        public static readonly string TAG = "Song";
        /// <summary>
        /// 歌曲信息录入成功
        /// </summary>
        public static readonly string CreatedMsg = "歌曲信息录入成功";
        public static readonly string UpdatedMsg = "歌曲信息更新成功";
        public static readonly string DeletedMsg = "歌曲信息删除成功";
    }

    /// <summary>
    /// 专辑常量
    /// </summary>
    public static class Album
    {
        public static readonly string TAG = "Album";

    }

    /// <summary>
    /// 歌手常量
    /// </summary>
    public static class Artist
    {
        public static readonly string TAG = "Artist";
    }

    /// <summary>
    /// 歌单常量
    /// </summary>
    public static class PlayList
    {
        public static readonly string TAG = "PlayList";
        /// <summary>
        /// 歌单类型相关的常量
        /// </summary>
        public static class Type
        {
            /// <summary>
            /// 推荐歌单 Define.PlayList.Type.Recommend
            /// </summary>
            public static readonly string Recommend = "Recommend";

            /// <summary>
            /// 收藏歌单
            /// </summary>
            public static readonly string Collection = "Collection";

            /// <summary>
            /// 喜欢歌单
            /// </summary>
            public static readonly string Like = "Like";

            /// <summary>
            /// 创建歌单
            /// </summary>
            public static readonly string Create = "Create";
        }

        /// <summary>
        /// 找不到歌单，可能是注册时未创建默认歌单
        /// </summary>
        public static readonly string NotFoundMsg = "找不到歌单，可能是注册时未创建默认歌单";
    }

    /// <summary>
    /// 文本格式化
    /// </summary>
    public static class Format
    {
        public static readonly string FullTime = "yyyy-MM-dd mm:ss.FFFFFFK";
    }

    /// <summary>
    /// 响应体相关的常量
    /// </summary>
    public static class Response
    {
        /// <summary>
        /// 用户未找到
        /// </summary>
        /// <typeparam name="TSrc"></typeparam>
        /// <param name="response">响应体</param>
        /// <param name="src">未找到的用户</param>
        public static void UserNotFound<TSrc>([Required]ResponseMessage response, TSrc src)
        {
            response.Code = ResponseDefine.NotFound;
            response.Message += "\r\n" + User.NotFoundMsg;
            Console.WriteLine("WS------ NotFound for User: \r\n" +JsonHelper.ToJson(src));
        }

        /// <summary>
        /// 响应体包装
        /// </summary>
        /// <param name="response"></param>
        /// <param name="code"></param>
        /// <param name="msgAppend"></param>
        public static void Wrap([Required]ResponseMessage response, [Required]string code, [Required]string msgAppend)
        {
            response.Code = code;
            response.Message += string.IsNullOrWhiteSpace(msgAppend) ? "" : ("\r\n" + msgAppend);
        }

        public static void Append([Required]ResponseMessage response, [Required]string msgAppend)
        {
            response.Message += string.IsNullOrWhiteSpace(msgAppend) ? "" : ("\r\n" + msgAppend);
        }

        #region 请求成功
        /// <summary>
        /// 成功
        /// </summary>
        public static readonly string SuccessCode = "0";
        public static readonly string SuccessMsg = "成功";

        #endregion

        #region POST

        /// <summary>
        /// 成功但是重复
        /// </summary>
        public static readonly string PostRepeatCode = "601";
        public static readonly string PostRepeatMsg = "创建资源重复，服务器的资源已经被创建";

        #endregion

        #region 模型错误

        /// <summary>
        /// 模型验证失败
        /// </summary>
        public static readonly string ModelStateInvalidCode = "100";
        public static readonly string ModelStateInvalidMsg = "模型验证失败";

        /// <summary>
        /// 参数（请求体）不能为空
        /// </summary>
        public static readonly string ArgumentNullErrorCode = "101";
        public static readonly string ArgumentNullErrorMsg = "参数不能为空";

        #endregion

        #region 请求成功

        public static readonly string CreatedCode = "201";
        /// <summary>
        /// 新的资源已经依据请求的需要而建立
        /// </summary>
        public static readonly string CreatedMsg = "新的资源已经依据请求的需要而建立";

        #endregion

        // 206 PartialContent 部分内容

        #region 请求错误

        /// <summary>
        /// 请求体错误
        /// </summary>
        public static readonly string BadRequsetCode = "400";
        public static readonly string BadRequsetMsg = "请求体错误";

        /// <summary>
        /// 找不到你要的资源
        /// </summary>
        public static readonly string NotFoundCode = "404";
        public static readonly string NotFoundMsg = "找不到你要的资源";

        /// <summary>
        /// 你没有权限访问该资源
        /// </summary>
        public static readonly string NotAllowCode = "403";
        public static readonly string NotAllowMsg = "你没有权限访问该资源";

        #endregion

        #region 服务器错误

        /// <summary>
        /// 服务器出现了异常
        /// </summary>
        public static readonly string ServiceErrorCode = "500";
        public static readonly string ServiceErrorMsg = "服务器出现了异常";

        /// <summary>
        /// 服务器不支持完成请求所需的功能
        /// </summary>
        public static readonly string NotSupportCode = "501";
        /// <summary>
        /// 服务器不支持完成请求所需的功能
        /// </summary>
        public static readonly string NotSupportMsg = "服务器不支持完成请求所需的功能";

        #endregion
    }
}