using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Core.DBModel;

namespace WL.Account.Core.DB
{
    /// <summary>
    /// 模块
    /// </summary>
    [Table("WL_SYSMODEL")]
    public class SysModuleDBModel : BaseDBModel
    {
        /// <summary>
        /// 模块编号
        /// </summary>
        public string ModuleID { get; set; } = string.Empty;
        /// <summary>
        /// 模块父级编号
        /// </summary>
        public string ModulePID { get; set; } = string.Empty;
        /// <summary>
        /// 模块id路径
        /// </summary>
        public string ModuleIDPath { get; set; } = string.Empty;
        /// <summary>
        /// 模块别名
        /// </summary>
        public string ModuleAlias { get; set; } = string.Empty;
        /// <summary>
        /// 模块别名路径
        /// </summary>
        public string ModulePath { get; set; } = string.Empty;
        /// <summary>
        /// 模块名
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;
        /// <summary>
        /// 模块备注
        /// </summary>
        public string ModuleRemark { get; set; } = string.Empty;
        /// <summary>
        /// 模块AppKey
        /// </summary>
        public string ModuleAppKey { get; set; } = string.Empty;
        /// <summary>
        /// 模块接口 类名称 完整名称，待定
        /// </summary>
        public string ModuleInterface { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string UrlPath { get; set; } = string.Empty;
    }
}
