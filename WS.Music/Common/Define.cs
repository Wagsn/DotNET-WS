using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Common
{
    /// <summary>
    /// 用户歌单常量池
    /// </summary>
    public static class PlayListType
    {
        public static readonly string Recommend = "Recommend";
        public static readonly string Collection = "Collection";
        public static readonly string Like = "Like";
        public static readonly string Create = "Create";

    }

    /// <summary>
    /// 歌单相关的常量
    /// </summary>
    public static class PlayListDefine
    {
        /// <summary>
        /// 歌单类型相关的常量
        /// </summary>
        public static class Type
        {
            /// <summary>
            /// 推荐歌单
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

    
}

/// <summary>
/// 项目专用的总常量池，TODO：将所有常量迁移到这里来，不过这样会导致其它地方不好创建常量，建议一个模块一个常量池类
/// </summary>
namespace WS.Music.Define
{
    public static class User
    {
        /// <summary>
        /// 找不到用户，可能是还未创建，请你检查一下你的请求是否正确
        /// </summary>
        public static readonly string NotFoundMsg = "找不到用户，可能是还未创建，请你检查一下你的请求是否正确";
    }

    public static class Song
    {
        /// <summary>
        /// 歌曲信息录入成功
        /// </summary>
        public static readonly string CreatedMsg = "歌曲信息录入成功";
        public static readonly string UpdatedMsg = "歌曲信息更新成功";
        public static readonly string DeletedMsg = "歌曲信息删除成功";
    }

    /// <summary>
    /// 歌单相关的常量
    /// </summary>
    public static class PlayList
    {
        /// <summary>
        /// 歌单类型相关的常量
        /// </summary>
        public static class Type
        {
            /// <summary>
            /// 推荐歌单
            /// </summary>
            public static readonly string Recommend = "Recommend";  // Define.PlayList.Type.Recommend
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
}