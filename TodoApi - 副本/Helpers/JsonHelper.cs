using System;

using Newtonsoft.Json;

namespace WS.Core.Helpers
{
    /// <summary>
    /// JSON助手，使用Newtonsoft.Json
    /// </summary>
    public static class JsonHelper
    {
        private static JsonSerializerSettings setting = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };

        static JsonHelper()
        {
            setting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }


        public static string ToJson(object obj)
        {
            if (obj == null)
                return "";

            return JsonConvert.SerializeObject(obj, setting);
        }

        public static object ToObject(string json, Type type)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return JsonConvert.DeserializeObject(json, type, setting);
        }

        public static TObject ToObject<TObject>(string json)
        {
            return (TObject)ToObject(json, typeof(TObject));
        }
    }
}
