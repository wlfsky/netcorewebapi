using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model.DB.SysApp
{
    public class SysCategoryDBModel
    {
        public string SysCategoryID { get; set; }
        public string SysCategoryRootID { get; set; }

        public string SysCategoryParentID { get; set; }
    }
}
