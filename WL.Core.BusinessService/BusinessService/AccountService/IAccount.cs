using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.BusinessService.BusinessService.AccountService
{
    public interface IAccount
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        string AccountID { get; set; }
        /// <summary>
        /// 账号名
        /// </summary>
        string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string Password { get; set; }

    }
}
