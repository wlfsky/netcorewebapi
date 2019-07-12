using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.i18n;

namespace WL.Core.Model.Language
{
    public static class DefLangKV
    {
        /*
         * key 结构 类别.类型.名字
         * 类别：Enum：枚举，Error：错误信息，Info：信息，Text：静态文字，直译（无key前缀文字就是key）
         * 
        */

        public static string DefLangKey => "def";

        public static LanguageKV LangKV = new LanguageKV()
        {
            ["Enum.SexEnum.None"] = "无",
            ["Enum.SexEnum.Male"] = "男",
            ["Enum.SexEnum.Female"] = "女",
            // --
            ["Error..NullData"] = "数据为空",
            ["Error..NullInput"] = "输入为空",
            // --
            ["Info..OPSuccess"] = "操作成功",
            ["Info..OPFail"] = "操作失败",
            // --
            ["Text..WebAppTitle"] = "我的网站",
            ["Text..WebAppKey"] = "网站",
            // --
            ["用户名"] = "用户名V",
            ["密码"] = "密码V"
        };
    }
}
