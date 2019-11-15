using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Core;
using WlToolsLib.DataShell;

namespace WL.Account.Core.Engine
{
    public interface IAccount : IAccountData
    {
        IDataShell<IAccountData> Create()
        {
            throw new NotImplementedException();
        }

        IDataShell<IAccountData> Regist();



        IDataShell<IAccountData> Login();
        IDataShell<IAccountData> Logout();



        IDataShell<IAccountData> EditInfo();

        IDataShell<IAccountData> ApplyForModifyPassword();

        IDataShell<IAccountData> ModifyPassword(ModifyPasswordReq req);


    }
}
