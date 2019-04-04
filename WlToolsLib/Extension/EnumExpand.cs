using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.Extension
{
    /// <summary>
    /// 枚举扩展方法类
    /// </summary>
    public static class EnumExpand
    {
        #region --枚举扩展--
        /// <summary>
        /// 枚举值翻译文字
        /// 应用场景：枚举值存储时采用的int类型，在有int的枚举值和枚举时可直接转换
        /// </summary>
        /// <param name="self"></param>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string EnumToStr(this Enum self, int enumValue)
        {
            var enumType = self.GetType();
            var result = enumType.GetEnumName(enumValue) ?? "None";
            return result;
        }
        #endregion

        /// <summary>
        /// 取得枚举编码
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int EnumCode<TEnum>(this TEnum self)
        {
            return self.GetHashCode();
        }

        /// <summary>
        /// 取得枚举名
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string EnumStr<TEnum>(this TEnum self)
        {
            return self.ToString();
        }
    }
}
