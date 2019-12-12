using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Enum;

namespace WL.Account.Core.Engine
{
    public interface IAccountData
    {
        string AccountID { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 高级密码
        /// </summary>
        string HighPsaaword { get; set; }
        /// <summary>
        /// 帐号关联电子邮件
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// 帐号关联老电子邮件
        /// </summary>
        string OldEmail { get; set; }
        /// <summary>
        /// 帐号关联手机号
        /// </summary>
        string Mobile { get; set; }
        /// <summary>
        /// 帐号关联老手机号
        /// </summary>
        string OldMobile { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        DateTime RegistTime { get; set; }
        /// <summary>
        /// 帐号状态（待注册验证，正常，安全异常，冻结，待再激活验证，销毁）
        /// </summary>
        AccountStatus Status { get; set; }
        /// <summary>
        /// 临时密码，验证码（短信，邮件）
        /// </summary>
        string TempPassword { get; set; }
        /// <summary>
        /// 临时密码，验证码过期时间
        /// </summary>
        DateTime TempPassOverTime { get; set; }
        /// <summary>
        /// 临时密码用途标记
        /// </summary>
        string TempPassUseFor { get; set; }
        /// <summary>
        /// 总登录次数
        /// </summary>
        int TotalLoginTimes { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        string ExtensionInfo { get; set; }
        /// <summary>
        /// 用户自己备注信息
        /// </summary>
        string Remark { get; set; }
        /// <summary>
        /// 系统备注信息
        /// </summary>
        string SysRemark { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        string UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// 用户昵称（通用昵称）
        /// </summary>
        string NickName { get; set; }
        /// <summary>
        /// 上个昵称
        /// </summary>
        string LastNickName { get; set; }
        /// <summary>
        /// 昵称头像
        /// </summary>
        string NickPic { get; set; }
        /// <summary>
        /// 真名
        /// </summary>
        string RealName { get; set; }
        /// <summary>
        /// 真脸图
        /// </summary>
        string RealFacePic { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        string IDCard { get; set; }
        /// <summary>
        /// 身份证图片
        /// </summary>
        string IDCardPic { get; set; }
        /// <summary>
        /// 地址信息
        /// </summary>
        string Address { get; set; }
        /// <summary>
        /// 主图片
        /// </summary>
        string MainPic { get; set; }
    }
}
