using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.DB;

namespace WL.Account.Core.Business
{
    public class SysMenu : SysMenuDBModel
    {
        public List<SysFunction> FunctionList { get; set; }
    }
}
