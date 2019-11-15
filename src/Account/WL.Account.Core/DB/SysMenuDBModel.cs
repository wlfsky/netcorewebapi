using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Account.Core.DB
{
    public class SysMenuDBModel
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string MenuID { get; set; } = string.Empty;
        /// <summary>
        /// 模块编号
        /// </summary>
        public string ModuleID { get; set; } = string.Empty;
        /// <summary>
        /// 系统菜单别名，简短别名
        /// </summary>
        public string MenuAlias { get; set; } = string.Empty;
        /// <summary>
        /// 菜单显示名字
        /// </summary>
        public string MenuName { get; set; } = string.Empty;
        /// <summary>
        /// 菜单方法
        /// </summary>
        public string MenuMethod { get; set; } = string.Empty;
        /// <summary>
        /// 菜单父级编号|
        /// </summary>
        public string MenuPID { get; set; } = string.Empty;
        /// <summary>
        /// 菜单编号路径|/id/id
        /// </summary>
        public string MenuIDPath { get; set; } = string.Empty;
        /// <summary>
        /// 菜单别名路径|/name/name
        /// </summary>
        public string MenuPath { get; set; } = string.Empty;
        public string UrlPath { get; set; } = string.Empty;
        /// <summary>
        /// 功能备注|
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
