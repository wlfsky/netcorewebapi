using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WL.Account.Model.Business;
using WL.Account.Model.DB;
using WL.Account.Model.DB.Interface;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.DataService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    
    public class UserAccountController : BaseApiController, IUserAccountDAL
    {


        private IUserAccountDAL _userDAL;


        public UserAccountController()
        {
            _userDAL = new UserAccountDAL();
        }

        [HttpGet]
        public string Ping(string xi)
        {
            return "{\"GBC\":\"X!a..." + xi + "118\"}";
        }

        [HttpPost]
        public DataShell<PageShell<AccountDBModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            Console.WriteLine("call data_service/useraccount/get");
            var res = _userDAL.GetPage(condition);
            return res;
        }

        [HttpPost]
        public DataShell<AccountDBModel> Get(AccountDBModel user)
        {
            Console.WriteLine("call data_service/useraccount/get");
            var res = _userDAL.Get(user);
            return res;
        }

        [HttpPost]
        public DataShell<AccountDBModel> Insert(AccountDBModel user)
        {
            var res = _userDAL.Insert(user);
            return res;
        }

        [HttpPost]
        public DataShell<AccountDBModel> Update(AccountDBModel user)
        {
            var res = _userDAL.Update(user);
            return res;
        }

        [HttpPost]
        public DataShell<int> Del(AccountDBModel user)
        {
            throw new NotImplementedException();
        }
    }
}
