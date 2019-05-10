using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WL.Core.Model;
using WlToolsLib.Pagination;
using WL.Account.Model.Business;

namespace WL.Account.BusinessService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserSystemBLL
    {
        DataShell<PageShell<UserAccount>> GetPage(PageCondition<UserAccount> userp);

        DataShell<UserAccount> Insert(UserAccount user);

        DataShell<UserAccount> Get(UserAccount user);

        DataShell<IEnumerable<UserAccount>> GetList(UserAccount user);

        DataShell<UserAccount> Update(UserAccount user);

        DataShell<int> Del(UserAccount user);

    }
}
