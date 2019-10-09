using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.CryptoHelper;
using WlToolsLib.Extension;

namespace WL.Account.BusinessService.Common
{
    public static class CommonLib
    {
        /// <summary>
        /// 用户帐号密码加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string AccountPassword(string password)
        {
            return password.ToSHA256();
        }

        /// <summary>
        /// 合法的帐号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool LegalAccount(this string account)
        {
            var res = @"[a-zA-Z0-9_]{5,35}".RegexIsMatch(account);
            return res;
        }

        /// <summary>
        /// 合法的电子邮件
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool LegalEmail(this string email)
        {
            var res = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$".RegexIsMatch(email);
            return res;
        }

        /// <summary>
        /// 合法的手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool LegalMobile(this string mobile)
        {
            var res = @"^1[3-9]{1}[0-9]{9}$".RegexIsMatch(mobile);
            return res;
        }
    }
}
