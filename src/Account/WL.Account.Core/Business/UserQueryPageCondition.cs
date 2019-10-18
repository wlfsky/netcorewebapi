using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Enum;

namespace WL.Account.Core.Business
{
    public class UserQueryPageCondition
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
        /// 帐号关联老电子邮件
        /// </summary>
        public string OldEmail { get; set; }
        /// <summary>
        /// 帐号关联手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 帐号关联老手机号
        /// </summary>
        public string OldMobile { get; set; }
        /// <summary>
        /// 注册时间起
        /// </summary>
        public DateTime? RegistTimeBegin { get; set; }
        /// <summary>
        /// 注册时间止
        /// </summary>
        public DateTime? RegistTimeEnd { get; set; }
        /// <summary>
        /// 帐号状态（待注册验证，正常，安全异常，冻结，待再激活验证，销毁）
        /// </summary>
        public AccountStatus? Status { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户昵称（通用昵称）
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 上个昵称
        /// </summary>
        public string LastNickName { get; set; }
        /// <summary>
        /// 真名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCard { get; set; }
    }
}
