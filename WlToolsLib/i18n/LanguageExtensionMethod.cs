using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.Expand;

namespace WlToolsLib.i18n
{
    public static class LanguageExtensionMethod
    {
        public static LanguageKV CurrOrDefLang()
        {
            LanguageDic lang_dic = new LanguageDic();
            LanguageKV curr_lang = new LanguageKV();
            curr_lang.Add("Enum.SexEnum.None", "无");
            curr_lang.Add("Enum.SexEnum.Male", "男");
            curr_lang.Add("Enum.SexEnum.Female", "女");
            lang_dic.Add("zh-cn", curr_lang);

            // ===============
            LanguageKV curr_lang2 = new LanguageKV();
            curr_lang2.Add("Enum.SexEnum.None", "None");
            curr_lang2.Add("Enum.SexEnum.Male", "Male");
            curr_lang2.Add("Enum.SexEnum.Female", "Female");
            lang_dic.Add("us-en", curr_lang2);
            return lang_dic.CurrOrDefLang();
        }

        public static string EnumToLangStr<TEnum>(this TEnum self)
        {
            var curr_lang = CurrOrDefLang();
            var name = self.GetType().Name;
            var key = "Enum.{0}.{1}".FormatStr(name, self);
            var value = self.ToString();
            if (curr_lang.ContainsKey(key))
            {
                value = curr_lang[key];
            }
            return value;
        }
    }
}
