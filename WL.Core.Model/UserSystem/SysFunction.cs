
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// 系统功能表
    /// </summary>
    [Table("WL_Function")]
    public class SysFunction : BaseModel
    {
        /// <summary>
        /// 功能编号|
        /// </summary>
        public int FuncID { get; set; } = 0;
        /// <summary>
        /// 功能名字|
        /// </summary>
        public string FuncName { get; set; } = string.Empty;
        /// <summary>
        /// 功能父级编号|
        /// </summary>
        public int FuncPID { get; set; } = 0;
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
