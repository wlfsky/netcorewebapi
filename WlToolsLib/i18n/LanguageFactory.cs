using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.Extension;
using WlToolsLib.JsonHelper;

namespace WlToolsLib.i18n
{
    public class LanguageFactory
    {
        /// <summary>
        /// 静态语言
        /// </summary>
        private static LanguageDic CurrLangDic;

        /// <summary>
        /// 获取当前全部语言
        /// </summary>
        /// <returns></returns>
        public static LanguageDic AllLang()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

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

        /// <summary>
        /// 获取当前语全部项
        /// </summary>
        /// <param name="defLangKV"></param>
        /// <returns></returns>
        public static LanguageKV CurrLang(LanguageKV defLangKV = null)
        {
            var all_lang = AllLang();
            var curr_lang = GetCurrLang(all_lang, defLangKV);
            return curr_lang;
        }

        /// <summary>
        /// 获取当前语言，全部语言来自外部
        /// </summary>
        /// <param name="allLang"></param>
        /// <param name="defLangKV"></param>
        /// <returns></returns>
        private static LanguageKV GetCurrLang(LanguageDic allLang, LanguageKV defLangKV = null)
        {
            var all_lang = allLang;
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

        /// <summary>
        /// 查找当前语言的对应文字。
        /// 如果不存在此项会自动添加并存储
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defLangKV"></param>
        /// <returns></returns>
        public static string Global(string key, LanguageKV defLangKV = null)
        {
            var all_lang = AllLang();
            var curr_lang = GetCurrLang(all_lang, defLangKV);
            if (curr_lang.ContainsKey(key))
            {
                return curr_lang[key];
            }
            all_lang.AddKV(key, key).Save();
            return key;
        }
    }
}
