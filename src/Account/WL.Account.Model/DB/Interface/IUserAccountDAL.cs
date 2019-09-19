using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Model.Business;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.Model.DB.Interface
{
    public interface IUserAccountDAL
    {
        DataShell<AccountDBModel> Get(AccountDBModel user);

        //DataShell<IEnumerable<AccountDBModel>> GetList(AccountDBModel user);
        DataShell<PageShell<AccountDBModel>> GetPage(PageCondition<UserQueryPageCondition> condition);

        DataShell<AccountDBModel> Insert(AccountDBModel user);

        DataShell<AccountDBModel> Update(AccountDBModel user);

        DataShell<int> Del(AccountDBModel user);
    }
}
