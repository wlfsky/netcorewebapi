using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Business;

namespace WL.Account.Core.Engine
{
    public class RootAccount : Account
    {
        #region --角色权限体系--
        public List<Role> RolsList { get; set; }

        public new bool IsRootUser()
        {
            return this.AccountID == "999";
        }
        #endregion
    }
}
