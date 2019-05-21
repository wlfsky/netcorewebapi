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

        DataShell<UserAccountDBModel> Get(UserAccountDBModel user);

        //DataShell<IEnumerable<UserAccountDBModel>> GetList(UserAccountDBModel user);

        //DataShell<PageShell<UserAccountDBModel>> GetPage(PageCondition<UserAccountDBModel> userp);

        //DataShell<UserAccountDBModel> Insert(UserAccountDBModel user);

        //DataShell<UserAccountDBModel> Update(UserAccountDBModel user);

        //DataShell<int> Del(UserAccountDBModel user);
    }
}
