using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Account.Model.Business;
using WL.Account.Model.DB;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.DataService
{
    public interface IUserAccountDAL
    {

        DataShell<AccountDBModel> Get(AccountDBModel user);

        //DataShell<IEnumerable<AccountDBModel>> GetList(AccountDBModel user);

        //DataShell<PageShell<AccountDBModel>> GetPage(PageCondition<AccountDBModel> userp);

        DataShell<AccountDBModel> Insert(AccountDBModel user);

        DataShell<AccountDBModel> Update(AccountDBModel user);

        DataShell<int> Del(AccountDBModel user);
    }
}
