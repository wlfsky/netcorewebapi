using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WL.Core.Model;

namespace WL.Core.DataService.UserSystem
{
    public interface IUserSystemDAL
    {

        DataShell<UserAccount> Get(UserAccount user);

        DataShell<IEnumerable<UserAccount>> GetList(UserAccount user);

        DataShell<IEnumerable<UserAccount>> GetPage(PageCondition<UserAccount> userp);

        DataShell<UserAccount> Insert(UserAccount user);

        DataShell<UserAccount> Update(UserAccount user);

        DataShell<int> Del(UserAccount user);

        DataShell<UserAccount> GetWithProject(UserAccount user);
    }
}
