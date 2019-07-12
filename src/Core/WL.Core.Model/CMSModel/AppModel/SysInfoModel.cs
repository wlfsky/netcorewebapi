using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Model.CMSModel.AppModel
{
    public class SysInfoModel
    {
        public bool IsDel { get; set; }
        public bool IsShow { get; set; }
        public int SysStatus { get; set; }
        public string SysStatusName { get; set; }
        public string SysRemark { get; set; }
        public string SysRemarkShow { get; set; }
    }
}
