// ------------------------------------
// ProjectName : WL.X.Common
// FileName    : ExtensionMethod
// CreateTime  : 2017/8/15 16:17:48
// Creator     : weilai
// Remark      : 
// ------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.Expand;

namespace WL.Core.Common
{
    /// <summary>
    /// ExtensionMethod,扩展方法
    /// </summary>
    public static class ExtensionMethod
    {
        //public static bool IsTrue(this bool self)
        //{
        //    return self == true;
        //}

        //public static bool IsFalse(this bool self)
        //{
        //    return self == false;
        //}

        /// <summary>
        /// 为IEnumerable<T>扩展一个Foreach功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> self, Func<T, T> func)
        {
            foreach (var item in self)
            {
                yield return func(item);
            }
        }

        /// <summary>
        /// 从右侧开始查找positionStr，提取字符串positionStr右侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string LastPositionRight(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return null; }
            var position = self.LastIndexOf(positionStr) + 1;
            if (position < 0) { return string.Empty; }
            var lastStr = self.Substring(position, self.Length - position);
            return lastStr;
        }

        /// <summary>
        /// 从右侧开始查找positionStr，提取字符串positionStr左侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string LastPositionLeft(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return null; }
            var position = self.LastIndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            var lastStr = self.Substring(0, position);
            return lastStr;
        }

        /// <summary>
        /// 从左则开始查找positionStr，提取字符串positionStr左侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string PositionLeft(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return null; }
            var position = self.IndexOf(positionStr);
            if (position < 0) { return string.Empty; }
            var lastStr = self.Substring(0, position);
            return lastStr;
        }

        /// <summary>
        /// 从左则开始查找positionStr，提取字符串positionStr右侧的字符串
        /// 未找到则返回空
        /// </summary>
        /// <param name="self"></param>
        /// <param name="positionStr"></param>
        /// <returns></returns>
        public static string PositionRight(this string self, string positionStr)
        {
            if (self.NullEmpty()) { return null; }
            var position = self.IndexOf(positionStr) + 1;
            if (position < 0) { return string.Empty; }
            var lastStr = self.Substring(position, self.Length - position);
            return lastStr;
        }

        /// <summary>
        /// Hashtable 扩展是否有值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool HasItem<TKey, TValue>(this Hashtable self)
        {
            if (self.IsNull())
            {
                return false;
            }
            var hasItem = false;
            foreach (var item in self)
            {
                hasItem = true;
                break;
            }
            return hasItem;
        }

        /// <summary>
        /// string的join的简化写法
        /// </summary>
        /// <param name="self"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinByx(this IEnumerable<string> self, string separator = ",")
        {
            if (self.IsNull())
            {
                return null;
            }
            if (self.HasItem().IsFalse())
            {
                return "";
            }
            return string.Join(separator, self);
        }
    }
}
