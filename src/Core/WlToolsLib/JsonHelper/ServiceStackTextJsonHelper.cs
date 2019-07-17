using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace WlToolsLib.JsonHelper
{
    #region --ServiceStackText的json序列化，带有时间转化--
    /// <summary>
    /// 据说堪比亚光速json转换
    /// 还有一种光速的没有写。
    /// </summary>
    public class ServiceStackTextJsonHelper : IJsonHelper
    {
        public ServiceStackTextJsonHelper()
        {
        }

        public string Serialize(object obj)
        {
            //用ISO方式格式化
            //ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.DateHandler.ISO8601;
            //自定义格式化
            JsConfig<DateTime>.SerializeFn = time => time.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return JsonSerializer.SerializeToString(obj, obj.GetType());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize<T>(T obj)
        {
            //用ISO方式格式化
            //ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.DateHandler.ISO8601;
            //自定义格式化
            JsConfig<DateTime>.SerializeFn = time => time.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return JsonSerializer.SerializeToString(obj, obj.GetType());
        }

        /// <summary>
        /// 序列化json，带有显示名单和 忽略名单。
        /// 注意此方法显示名单无效，忽略名单功能有效
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="j_data"></param>
        /// <param name="showFields"></param>
        /// <param name="ignoreFields"></param>
        /// <returns></returns>
        public string Serialize<TType>(TType obj, IList<string> showFields = null, IList<string> ignoreFields = null)
        {
            if (ignoreFields != null && ignoreFields.Any())
            {
                JsConfig<TType>.ExcludePropertyNames = ignoreFields.ToArray();
            }
            #region --还有一写法--
            // 1.数据成员特性法
            // 2.数据成员忽略特性法
            // 3.临时忽略名单法（上面就用的此方法）
            // 4.类型内定义 特定方法 法
            // --------------------------
            /* 1.[DataMember]
             * 2.[IgnoreDataMember]
             * 3.JsConfig<TType>.ExcludePropertyNames = new [] { "IsIgnored" };
             * 4.public bool? ShouldSerialize(string fieldName) { return fieldName == "IsIgnored"; }
             */
            #endregion
            JsConfig<DateTime>.SerializeFn = time => time.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return JsonSerializer.SerializeToString(obj, obj.GetType());
        }

        public T Deserialize<T>(string jsonStr)
        {
            //用ISO方式格式化
            //ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.DateHandler.ISO8601;
            //自定义格式化
            JsConfig<DateTime>.DeSerializeFn = timeStr =>
            {
                System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss.fff";
                return Convert.ToDateTime(timeStr, dtFormat);
            };
            return JsonSerializer.DeserializeFromString<T>(jsonStr);
        }
    }
    #endregion
}
