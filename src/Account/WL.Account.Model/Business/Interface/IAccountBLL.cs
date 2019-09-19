using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Model.DB;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.Model.Business.Interface
{
    public interface IAccountBLL
    {
        DataShell<AccountModel> Get(AccountModel user);
        DataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition);

        //DataShell<UserAccount> Insert(UserAccount user);



        //DataShell<IEnumerable<UserAccount>> GetList(UserAccount user);

        //DataShell<UserAccount> Update(UserAccount user);

        //DataShell<int> Del(UserAccount user);
    }
}
