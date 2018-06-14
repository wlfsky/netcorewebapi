using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.Expand;
using WlToolsLib.JsonHelper;

namespace WlToolsLib.i18n
{
    public class LanguageFactory
    {

        private static LanguageDic CurrLangDic;
        /// <summary>
        /// 获取当前语言或默认语言
        /// </summary>
        /// <returns></returns>
        public static LanguageDic AllLang()
        {
            string base_path = "F:\\github\\netcorewebapi\\WL.Core.ConsoleApp\\i18n\\";
            if (CurrLangDic == null)
            {
                CurrLangDic = new LanguageDic();
                var files = System.IO.Directory.GetFiles(base_path);
                if (files.HasItem())
                {
                    foreach (var item in files)
                    {
                        var lang_kv_json = item.GetTextFile();
                        if (lang_kv_json.NotNullEmpty())
                        {
                            LanguageKV curr_kv = lang_kv_json.ToObj<LanguageKV>();
                            var key = item.RightSinceStr("\\", ".");
                            CurrLangDic.Add(key, curr_kv);
                        }
                    }
                }
            }
            return CurrLangDic;
        }

        public static LanguageKV CurrLang(LanguageKV defLangKV = null)
        {
            var all_lang = AllLang();
            if (!all_lang.ContainsKey(all_lang.DefLang) && defLangKV.NotNull())
            {
                all_lang.Add(all_lang.DefLang, defLangKV);
            }
            if (all_lang.ContainsKey(all_lang.CurrLang))
            {
                return all_lang[all_lang.CurrLang];
            }
            else
            {
                return all_lang[all_lang.DefLang];
            }
        }
    }
}
