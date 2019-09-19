using Dapper.Contrib.Extensions;
using System;
using WL.Account.Model.Enum;
using WL.Core.DBModel;

namespace WL.Account.Model.DB
{
    /// <summary>
    /// 帐号数据模型
    /// </summary>
    [Table("WL_ACCOUNT")]
    public class AccountDBModel : BaseDBModel
    {
        /// <summary>
        /// 帐号编号
        /// </summary>
        [Key]
        public string AccountID { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 高级密码
        /// </summary>
        public string HighPsaaword { get; set; }
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
        /// 注册时间
        /// </summary>
        public DateTime RegistTime { get; set; }
        /// <summary>
        /// 帐号状态（待注册验证，正常，安全异常，冻结，待再激活验证，销毁）
        /// </summary>
        public AccountStatus Status { get; set; }
        /// <summary>
        /// 临时密码，验证码（短信，邮件）
        /// </summary>
        public string TempPassword { get; set; }
        /// <summary>
        /// 临时密码，验证码过期时间
        /// </summary>
        public DateTime TempPassOverTime { get; set; }
        /// <summary>
        /// 临时密码用途标记
        /// </summary>
        public string TempPassUseFor { get; set; }
        /// <summary>
        /// 总登录次数
        /// </summary>
        public int TotalLoginTimes { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExtensionInfo { get; set; }
        /// <summary>
        /// 用户自己备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 系统备注信息
        /// </summary>
        public string SysRemark { get; set; }
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
        /// <summary>
        /// 地址信息
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 主图片
        /// </summary>
        public string MainPic { get; set; }
    }
}
