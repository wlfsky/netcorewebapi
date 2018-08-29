
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// 用户表
    /// </summary>
    [Table("WL_ACCOUNT")]
    public class UserAccount : BaseModel
    {
        /// <summary>
        /// 用户编号|
        /// </summary>
        [Key]
        //[Required]
        public long UserID { get; set; } = 0;
        /// <summary>
        /// 用户登录名|
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 用户密码|
        /// </summary>
        public string UserPassword { get; set; } = string.Empty;
        /// <summary>
        /// 用户手机号|
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 用户昵称|
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 用户姓名|
        /// </summary>
        public string RealName { get; set; } = string.Empty;
        /// <summary>
        /// 用户电子邮件|
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 二次密码|
        /// </summary>
        public string HighPsaaword { get; set; } = string.Empty;
        /// <summary>
        /// 临时密码用于找回密码|
        /// </summary>
        public string TempPassword { get; set; } = string.Empty;
        /// <summary>
        /// 临时密码失效时间|
        /// </summary>
        public DateTime TempPassOverTime { get; set; } = new DateTime();
        /// <summary>
        /// 注册时间|
        /// </summary>
        public DateTime RegistTime { get; set; } = new DateTime();
        /// <summary>
        /// 登录次数|
        /// </summary>
        public float LoginTimes { get; set; } = 0f;
        /// <summary>
        /// 最后登录时间|
        /// </summary>
        public DateTime LastLoginTime { get; set; } = new DateTime();
        /// <summary>
        /// 关联职员编号|
        /// </summary>
        public string EmployeeID { get; set; } = string.Empty;
        /// <summary>
        /// 关联项目编号|
        /// </summary>
        // public string ProjectID { get; set; } = string.Empty;
        /// <summary>
        /// 帐号是否禁用|0未禁用1禁用
        /// </summary>
        public int IsForbid { get; set; } = 0;

    }
}
