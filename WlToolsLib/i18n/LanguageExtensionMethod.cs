using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.Expand;
using WlToolsLib.JsonHelper;

namespace WlToolsLib.i18n
{
    public static class LanguageExtensionMethod
    {
        

        public static string EnumToLangStr<TEnum>(this TEnum self)
        {
            var curr_lang = LanguageFactory.CurrLang();
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
