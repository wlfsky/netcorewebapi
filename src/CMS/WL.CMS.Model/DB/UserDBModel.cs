using System;
using System.Collections.Generic;
using System.Text;
using WL.CMS.Model.Enum;

namespace WL.CMS.Model.DB
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserDBModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 帐号id
        /// </summary>
        public string AccountID { get; set; }
        /// <summary>
        /// 用户帐号
        /// </summary>
        public string UserAccount { get; set; }
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
        /// 真名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 地址信息
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus Status { get; set; }
        /// <summary>
        /// 主图片
        /// </summary>
        public string MainPic { get; set; }
    }
}
