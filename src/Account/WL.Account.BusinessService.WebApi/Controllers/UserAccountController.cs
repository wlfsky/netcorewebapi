using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WL.Account.Model.Business;
using WL.Account.Model.Business.Interface;
using WlToolsLib.DataShell;

namespace WL.Account.BusinessService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserAccountController : BaseApiController
    {
        private IAccountBLL _userBll;


        public UserAccountController()
        {
            _userBll = new AccountBLL();
        }

        [HttpGet]
        public DataShell<string> Ping(string x)
        {
            return "ok".Succ();
        }

        /// <summary>
        /// 用户.业务层.获取账户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public DataShell<AccountModel> Get(AccountModel user)
        {
            Console.WriteLine("call business_service/useraccount/get");
            var res = _userBll.Get(user);
            return res;
        }

        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
