using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.DB;

namespace WL.Account.Core.Business
{
    public class Role : RoleDBModel
    {
        public List<SysModule> ModuleList { get; set; }
    }
}
