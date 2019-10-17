using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Account.Core.Core
{
    public class AccountLoginReq
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 帐号关联电子邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 帐号关联手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
