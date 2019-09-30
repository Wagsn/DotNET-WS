using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Net.Http
{
    /// <summary>
    /// HTTP方法
    /// </summary>
    public class HttpMethod
    {
        public static readonly string GET = nameof(GET);
        public static readonly string POST = nameof(POST);
        public static readonly string PUT = nameof(PUT);
        public static readonly string DELETE = nameof(DELETE);
        public static readonly string HEAD = nameof(HEAD);
        public static readonly string OPTIONS = nameof(OPTIONS);
        public static readonly string PATCH = nameof(PATCH);
        public static readonly string COPY = nameof(COPY);
        public static readonly string LINK = nameof(LINK);
        public static readonly string UNLINK = nameof(UNLINK);
        public static readonly string PURGE = nameof(PURGE);
        public static readonly string LOCK = nameof(LOCK);
        public static readonly string UNLOCK = nameof(UNLOCK);
        public static readonly string PROPFIND = nameof(PROPFIND);
        public static readonly string VIEW = nameof(VIEW);
    }
}
