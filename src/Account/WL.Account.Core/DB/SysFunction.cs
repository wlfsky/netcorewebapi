
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
    /// 系统功能表
    /// </summary>
    [Table("WL_Function")]
    public class SysFunction : BaseDBModel
    {
        /// <summary>
        /// 功能编号|
        /// </summary>
        public string FuncID { get; set; } = string.Empty;
        /// <summary>
        /// 系统功能别名，简短别名
        /// </summary>
        public string FuncAlias { get; set; } = string.Empty;
        /// <summary>
        /// 功能名字|
        /// </summary>
        public string FuncName { get; set; } = string.Empty;
        /// <summary>
        /// 功能父级编号|
        /// </summary>
        public string FuncPID { get; set; } = string.Empty;
        /// <summary>
        /// 功能编号路径|/id/id
        /// </summary>
        public string FuncIDPath { get; set; } = string.Empty;
        /// <summary>
        /// 功能别名路径|/name/name
        /// </summary>
        public string FuncPath { get; set; } = string.Empty;
        /// <summary>
        /// 功能备注|
        /// </summary>
        public string Remark { get; set; } = string.Empty;

    }
}
