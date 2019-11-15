using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Business;
using WL.Account.Core.Core;
using WL.Account.Core.DB;
using WlToolsLib.DataShell;

namespace WL.Account.Core.Engine
{
    public class Account : AccountDBModel, IAccount
    {
        #region ----

        #endregion

        #region --角色权限体系--
        public void CurrRoles()
        {

        }
        public List<Role> RolsList { get; set; }

        public bool IsRootUser()
        {
            return false;
        }

        public IDataShell<IAccountData> Regist()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> Login()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> Logout()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> EditInfo()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> ApplyForModifyPassword()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> ModifyPassword(ModifyPasswordReq req)
        {
            throw new NotImplementedException();
        }


        #endregion


    }
}
