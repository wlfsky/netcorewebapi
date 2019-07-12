using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.DataShell;

namespace WL.Account.Model.Business.Interface
{
    public interface IAccountBLL
    {
        DataShell<AccountModel> Get(AccountModel user);
        //DataShell<PageShell<UserAccount>> GetPage(PageCondition<UserAccount> userp);

        //DataShell<UserAccount> Insert(UserAccount user);



        //DataShell<IEnumerable<UserAccount>> GetList(UserAccount user);

        //DataShell<UserAccount> Update(UserAccount user);

        //DataShell<int> Del(UserAccount user);
    }
}
