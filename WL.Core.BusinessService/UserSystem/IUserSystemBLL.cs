using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WL.Core.Model;
using WL.Core.Model.UserSystem;
using WlToolsLib.Pagination;

namespace WL.Core.BusinessService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserSystemBLL
    {
        DataShell<IEnumerable<UserAccount>> GetPage(PageCondition<UserAccount> userp);

        DataShell<UserAccount> Insert(UserAccount user);

        DataShell<UserAccount> Get(UserAccount user);

        DataShell<IEnumerable<UserAccount>> GetList(UserAccount user);

        DataShell<UserAccount> Update(UserAccount user);

        DataShell<int> Del(UserAccount user);

        DataShell<UserFullInfo> GetUserFullInfo(UserAccount user);

        DataShell<UserAccount> GetWithProject(UserAccount user);
    }
}
