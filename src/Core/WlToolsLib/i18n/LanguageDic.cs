﻿using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.Extension;
using WlToolsLib.JsonHelper;

namespace WlToolsLib.i18n
{
    /// <summary>
    /// 语言和全球容器
    /// </summary>
    public class LanguageDic : Dictionary<string, LanguageKV>
    {
        public LanguageDic()
        {
            this.CurrLang = "zh-cn";
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

        /// <summary>
        /// 添加一个语言项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public LanguageDic AddKV(string key, string value)
        {
            foreach (var i in this.Keys)
            {
                if (!this[i].ContainsKey(key))
                {
                    this[i].Add(key, value);
                }
            }
            return this;
        }

        /// <summary>
        /// 存储所有语言
        /// </summary>
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
