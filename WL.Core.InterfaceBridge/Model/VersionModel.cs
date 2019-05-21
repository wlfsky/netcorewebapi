using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.Model
{
    public class VersionModel
    {
        public string ModuleID { get; set; }
        public string VersionID { get; set; }
        public int Version { get; set; }
        public string VersionName { get; set; }
        public string VersionTitle { get; set; }
        public List<string> VersionRemarks { get; set; }
    }
}
