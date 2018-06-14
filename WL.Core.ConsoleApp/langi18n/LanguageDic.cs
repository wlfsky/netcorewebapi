using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.ConsoleApp
{
    public class LanguageDic : Dictionary<string, LanguageKV>
    {
        public LanguageDic()
        {
            this.CurrLang = "us-en";
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
