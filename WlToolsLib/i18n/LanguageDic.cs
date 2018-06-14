using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.Expand;
using WlToolsLib.JsonHelper;

namespace WlToolsLib.i18n
{
    public class LanguageDic : Dictionary<string, LanguageKV>
    {
        public LanguageDic()
        {
            this.CurrLang = "us-en";
            this.DefLang = "def";
        }

        /// <summary>
        /// 默认语言设置
        /// </summary>
        public string DefLang { get; set; }

        /// <summary>
        /// 当前语言设置
        /// </summary>
        public string CurrLang { get; set; }

        public void Save()
        {
            string path = "F:\\github\\netcorewebapi\\WL.Core.ConsoleApp\\i18n\\";
            foreach (var key in this.Keys)
            {
                var json = this[key].ToJson();
                var p = "{0}{1}.json".FormatStr(path, key);
                using (var s = System.IO.File.CreateText(p))
                {
                    s.Write(json);
                }
            }
        }
    }
}
