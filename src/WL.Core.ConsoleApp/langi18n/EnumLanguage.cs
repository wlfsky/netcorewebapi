using System;
using System.Collections.Generic;
using System.Text;
using static WL.Core.ConsoleApp.KVEntityNew;

namespace WL.Core.ConsoleApp
{
    public class EnumLanguage
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SexEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 男
        /// </summary>
        Male = 1,
        /// <summary>
        /// 女
        /// </summary>
        Female = 2,
    }

    public static class SexExtensionMethod
    {
        public static List<CodeNameMap<SexEnum, int, string>> SexKV = new List<CodeNameMap<SexEnum, int, string>>()
        {
            new CodeNameMap<SexEnum, int, string>() {  Enum = SexEnum.None, Code = SexEnum.None.GetHashCode(), Name ="无"},
            new CodeNameMap<SexEnum, int, string>() {  Enum = SexEnum.Male, Code = SexEnum.Male.GetHashCode(), Name ="男"},
            new CodeNameMap<SexEnum, int, string>() {  Enum = SexEnum.Female, Code = SexEnum.Female.GetHashCode(), Name ="女"}
        };

        public static int EnumCode<TEnum>(this TEnum self)
        {
            return self.GetHashCode();
        }
        public static string EnumStr<TEnum>(this TEnum self)
        {
            return self.ToString();
        }
    }

}

