using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    public class SysInfoModel
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 系统状态
        /// </summary>
        public int SysStatus { get; set; }
        /// <summary>
        /// 系统状态名称
        /// </summary>
        public string SysStatusName { get; set; }
        /// <summary>
        /// 系统备注
        /// </summary>
        public string SysRemark { get; set; }
        /// <summary>
        /// 系统备注显示？
        /// </summary>
        public string SysRemarkShow { get; set; }
    }
}
