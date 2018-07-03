using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Core.Model;
using WL.Core.DataService;
using Autofac;
using WlToolsLib.DataShell;
using WL.Core.Model.UserSystem;
using WL.Core.DataService.UserSystem;
using WlToolsLib.Pagination;

namespace WL.Core.BusinessService
{
    /// <summary>
    /// 
    /// </summary>
    public class UserSystemBLL : BaseBLL, IUserSystemBLL
    {

        private IUserSystemDAL _userDAL;

        public UserSystemBLL()
        {
            _userDAL = Container.Resolve<IUserSystemDAL>();
        }

        public DataShell<UserAccount> Insert(UserAccount user)
        {
            return _userDAL.Insert(user);
        }

        public DataShell<UserAccount> Get(UserAccount user)
        {
            return _userDAL.Get(user);
        }

        public DataShell<IEnumerable<UserAccount>> GetList(UserAccount user)
        {
            return _userDAL.GetList(user);
        }

        public DataShell<IEnumerable<UserAccount>> GetPage(PageCondition<UserAccount> userp)
        {
            return _userDAL.GetPage(userp);
        }

        public DataShell<UserAccount> Update(UserAccount user)
        {
            return _userDAL.Update(user);
        }

        public DataShell<int> Del(UserAccount user)
        {
            return _userDAL.Del(user);
        }

        public DataShell<UserFullInfo> GetUserFullInfo(UserAccount user)
        {
            var result = new UserFullInfo()
            {
                UserAccountInfo = user,
                DepatmentInfo = new Department() { DepID = 1, DepName = "depwl" },
                EmployeeInfo = new Employee() { EmployeeID = 1, EmployeeName = "wlname", DepID = 1, DepName = "depwl" },
                ProjectInfo = new Project() { ProjectID = 1, ProjectName = "pname" },
                PowersInfo = new List<RolePower> { new RolePower() { PowerID = 1, RoleID = 1, CoreID = 1, DepID = 1, FuncID = 1 } },
                RolesInfo = new List<Role> { new Role() { RoleID = 1, RoleName = "open", CoreID = 1 } }
            };
            return result.Success();
        }

        public DataShell<UserAccount> GetWithProject(UserAccount user)
        {
            return _userDAL.GetWithProject(user);
        }

    }
}
