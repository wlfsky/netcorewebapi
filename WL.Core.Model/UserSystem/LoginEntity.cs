using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model.UserSystem
{
    /// <summary>
    /// 登陆提交实体
    /// </summary>
    public class LoginEntity
    {
        public int CoreID { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginAccount { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 登陆验证码
        /// </summary>
        public string VerifyCode { get; set; }
    }
}
