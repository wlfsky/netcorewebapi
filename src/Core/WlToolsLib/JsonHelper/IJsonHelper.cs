using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.JsonHelper
{
    #region --json序列化接口--
    /// <summary>
    /// json 序列化，反序列化接口
    /// </summary>
    public interface IJsonHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize<TType>(TType obj);
        /// <summary>
        /// 序列化成json，指定了显示字段，和忽略字段。优先处理显示字段，如果显示字段为空才处理忽略字段。
        /// 注意，此方法层级不敏感，就是说指定的字段名在任何层级都有效
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="obj"></param>
        /// <param name="showFields"></param>
        /// <param name="ignoreFields"></param>
        /// <returns></returns>
        string Serialize<TType>(TType obj, IList<string> showFields = null, IList<string> ignoreFields = null);
        /// <summary>
        /// 将json反序列化成对象
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="json_str"></param>
        /// <returns></returns>
        TType Deserialize<TType>(string json_str);
    }
    #endregion
}
