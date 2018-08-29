
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// 用户表
    /// </summary>
    [Table("WL_Employee")]
    public class Employee : BaseModel
    {
        /// <summary>
        /// 职员编号|
        /// </summary>
        public long EmployeeID { get; set; } = 0;
        /// <summary>
        /// 职员名字|
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;
        /// <summary>
        /// 性别|1男2女
        /// </summary>
        public int Sex { get; set; } = 0;
        /// <summary>
        /// 手机号|
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 身份证明类型|
        /// </summary>
        public int IDType { get; set; } = 0;
        /// <summary>
        /// 身份证明号码|
        /// </summary>
        public string IDNum { get; set; } = string.Empty;
        /// <summary>
        /// 生日|
        /// </summary>
        public DateTime Birthday { get; set; } = new DateTime();
        /// <summary>
        /// 年龄|
        /// </summary>
        public int Age { get; set; } = 0;
        /// <summary>
        /// 职务编号|
        /// </summary>
        public string DutiesID { get; set; } = string.Empty;
        /// <summary>
        /// 职务|
        /// </summary>
        public string DutiesName { get; set; } = string.Empty;
        /// <summary>
        /// 部门ID|
        /// </summary>
        public int DepID { get; set; } = 0;
        /// <summary>
        /// 部门名字|
        /// </summary>
        public string DepName { get; set; } = string.Empty;
        /// <summary>
        /// 部门路径|
        /// </summary>
        public string DepPath { get; set; } = string.Empty;
        /// <summary>
        /// 介绍|
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 是否在职|1在职2休假3离职4死亡
        /// </summary>
        public int JobStatus { get; set; } = 0;

    }
}
