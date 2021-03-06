
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Core.DBModel;

namespace WL.Account.Core.DB
{

    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    [Table("WL_UserPower")]
    public class UserPower : BaseDBModel
    {
        /// <summary>
        /// 用户角色关系id|
        /// </summary>
        public long URRID { get; set; } = 0;
        /// <summary>
        /// 用户编号|
        /// </summary>
        public long UserID { get; set; } = 0;
        /// <summary>
        /// 角色编号|
        /// </summary>
        public long RoleID { get; set; } = 0;
        /// <summary>
        /// 角色路径|
        /// </summary>
        public string RolePath { get; set; } = string.Empty;
        /// <summary>
        /// 用户角色映射图|格式为（用户id|/角色路径/）|用于快速搜索
        /// </summary>
        public string UserRoleMap { get; set; } = string.Empty;

    }
}
