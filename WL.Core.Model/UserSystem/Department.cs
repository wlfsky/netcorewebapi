
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// 部门表
    /// </summary>
    [Table("WL_Dep")]
    public class Department : BaseModel
    {
        /// <summary>
        /// 部门编号|
        /// </summary>
        public int DepID { get; set; } = 0;
        /// <summary>
        /// 部门名字|
        /// </summary>
        public string DepName { get; set; } = string.Empty;
        /// <summary>
        /// 部门父级编号|
        /// </summary>
        public int DepPID { get; set; } = 0;
        /// <summary>
        /// 部门编号路径|/id/id
        /// </summary>
        public string DepIDPath { get; set; } = string.Empty;
        /// <summary>
        /// 部门名字路径|/name/name
        /// </summary>
        public string DepNamePath { get; set; } = string.Empty;
        /// <summary>
        /// 部门备注|
        /// </summary>
        public string Remark { get; set; } = string.Empty;

    }
}
