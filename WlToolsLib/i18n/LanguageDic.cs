﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.i18n
{
    public class LanguageDic : Dictionary<string, LanguageKV>
    {
        public LanguageDic()
        {
            this.CurrLang = "zh-cn";
            this.DefLang = "zh-cn";
        }

        public string DefLang { get; set; }
        public string CurrLang { get; set; }

        public LanguageKV CurrOrDefLang()
        {
            if (this.ContainsKey(CurrLang))
            {
                return this[CurrLang];
            }
            else
            {
                return this[DefLang];
            }

        }
    }
}
