using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.Model
{
    /// <summary>
    /// 模块站点（模块-版本-站点）
    /// </summary>
    public class ModuleSiteModel
    {
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string SitePath { get; set; }
        public string ServiceType { get; set; } = "webapi";
        public int Version { get; set; }
        public string ModuleID { get; set; }
        public string VersionID { get; set; }
    }
}
