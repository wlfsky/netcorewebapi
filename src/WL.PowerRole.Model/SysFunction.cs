
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Core.DBModel;

namespace WL.Core.Model
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
        public long FuncID { get; set; } = 0;
        /// <summary>
        /// 系统功能代码，简短代码
        /// </summary>
        public string FuncCode { get; set; } = string.Empty;
        /// <summary>
        /// 功能名字|
        /// </summary>
        public string FuncName { get; set; } = string.Empty;
        /// <summary>
        /// 功能父级编号|
        /// </summary>
        public long FuncPID { get; set; } = 0;
        /// <summary>
        /// 功能编号路径|/id/id
        /// </summary>
        public string FuncIDPath { get; set; } = string.Empty;
        /// <summary>
        /// 功能名字路径|/name/name
        /// </summary>
        public string FuncNamePath { get; set; } = string.Empty;
        /// <summary>
        /// 功能备注|
        /// </summary>
        public string Remark { get; set; } = string.Empty;

    }
}
