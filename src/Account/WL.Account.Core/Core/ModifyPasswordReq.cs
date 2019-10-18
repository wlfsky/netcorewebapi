using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Account.Core.Core
{
    public class ModifyPasswordReq
    {
        /// <summary>
        /// 帐号编号
        /// </summary>
        public string AccountID { get; set; }
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
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string NewPassword { get; set; }
        /// <summary>
        /// 新密码2
        /// </summary>
        public string NewPassword2 { get; set; }
        /// <summary>
        /// 临时密码，验证码（短信，邮件）
        /// </summary>
        public string TempPassword { get; set; }
        /// <summary>
        /// 临时密码用途标记
        /// </summary>
        public string TempPassUseFor { get; set; }
    }
}
