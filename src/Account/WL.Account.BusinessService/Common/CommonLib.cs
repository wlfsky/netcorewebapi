using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.CryptoHelper;

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
    }
}
