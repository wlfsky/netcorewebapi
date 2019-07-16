using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.Extension
{
    /// <summary>
    /// 单例重复筛选器,该重复过滤器无法同时过滤多组数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepeatFilter<T>
    {
        /// <summary>
        /// 重复筛选器容器
        /// </summary>
        private static IDictionary<T, string> container { get; set; } = new Dictionary<T, string>();

        /// <summary>
        /// 单例
        /// </summary>
        private static RepeatFilter<T> Self = new RepeatFilter<T>();

        /// <summary>
        /// 单例入口
        /// </summary>
        /// <returns></returns>
        public static RepeatFilter<T> Instance()
        {
            if (Self.IsNull())
            {
                Self = new RepeatFilter<T>();
            }
            return Self;
        }

        /// <summary>
        /// 添加并过滤重复
        /// </summary>
        /// <param name="item"></param>
        /// <returns>isRepeat是否重复=true重复，=false不重复</returns>
        public (bool isRepeat, T repeatItem) InsertFilter(T item)
        {
            if (container.ContainsKey(item))
            {
                return (true, item);
            }
            else
            {
                container.Add(item, "");
                return (false, default(T));
            }
        }

        /// <summary>
        /// 清空重复过滤器
        /// </summary>
        /// <returns></returns>
        public bool Clear()
        {
            if (container.HasItem())
            {
                container.Clear();
            }
            return true;
        }
    }
}
