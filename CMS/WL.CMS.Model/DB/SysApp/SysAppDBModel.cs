using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model.DB.SysApp
{
    public class SysAppDBModel
    {
        public string SysAppID { get; set; }
        public string SysAppKey { get; set; }
        public string SysAppName { get; set; }

        public long SysAppVersion { get; set; }
    }
}
