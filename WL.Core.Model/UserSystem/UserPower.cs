
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    [Table("WL_UserPower")]
    public class UserPower : BaseModel
    {
        /// <summary>
        /// 用户角色关系id|
        /// </summary>
        public int URRID { get; set; } = 0;
        /// <summary>
        /// 用户编号|
        /// </summary>
        public int UserID { get; set; } = 0;
        /// <summary>
        /// 角色编号|
        /// </summary>
        public int RoleID { get; set; } = 0;
        /// <summary>
        /// 角色路径|
        /// </summary>
        public string RoleIDPath { get; set; } = string.Empty;
        /// <summary>
        /// 用户角色映射图|用于快速搜索
        /// </summary>
        public string UserRoleMap { get; set; } = string.Empty;

    }
}
