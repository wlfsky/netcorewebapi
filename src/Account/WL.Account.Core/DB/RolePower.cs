
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
    /// 角色部门功能关系表
    /// </summary>
    [Table("WL_RolePower")]
    public class RolePower : BaseDBModel
    {
        /// <summary>
        /// 权限编号|
        /// </summary>
        public long PowerID { get; set; } = 0;
        /// <summary>
        /// 部门编号|
        /// </summary>
        public long DepID { get; set; } = 0;
        /// <summary>
        /// 部门路径|/dep/dep
        /// </summary>
        public string DepPath { get; set; } = string.Empty;
        /// <summary>
        /// 角色编号|
        /// </summary>
        public long RoleID { get; set; } = 0;
        /// <summary>
        /// 角色路径|/role/role
        /// </summary>
        public string RolePath { get; set; } = string.Empty;
        /// <summary>
        /// 功能编号|
        /// </summary>
        public long FuncID { get; set; } = 0;
        /// <summary>
        /// 功能路径|/func/func
        /// </summary>
        public string FuncPath { get; set; } = string.Empty;
        /// <summary>
        /// 权限坐标：dep|role|func
        /// </summary>
        public string Coordinate { get; set; } = string.Empty;

    }
}
