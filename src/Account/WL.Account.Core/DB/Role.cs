
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
    /// 角色表
    /// </summary>
    [Table("WL_Role")]
    public class Role : BaseDBModel
    {
        /// <summary>
        /// 角色编号|
        /// </summary>
        public string RoleID { get; set; } = string.Empty;
        /// <summary>
        /// 角色别名|构成角色名路径
        /// </summary>
        public string RoleAlias { get; set; } = string.Empty;
        /// <summary>
        /// 角色名字|
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
        /// <summary>
        /// 角色父级编号|
        /// </summary>
        public string RolePID { get; set; } = string.Empty;
        /// <summary>
        /// 角色编号路径|/id/id
        /// </summary>
        public string RoleIDPath { get; set; } = string.Empty;
        /// <summary>
        /// 角色名字路径|/name/name
        /// </summary>
        public string RolePath { get; set; } = string.Empty;
        /// <summary>
        /// 角色备注|
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 是否继承权限|0不继承1继承
        /// </summary>
        public int IsInherit { get; set; } = 0;

    }
}
