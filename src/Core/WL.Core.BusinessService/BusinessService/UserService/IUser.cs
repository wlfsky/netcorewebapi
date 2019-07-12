using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.BusinessService.BusinessService.UserService
{
    public interface IUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        string UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }
    }
}
