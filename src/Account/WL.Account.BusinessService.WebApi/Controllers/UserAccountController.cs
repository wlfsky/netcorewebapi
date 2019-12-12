using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WL.Account.Core.Business;
using WL.Account.Core.Business.Interface;
using WL.Account.Core.Core;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.BusinessService.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserAccountController : BaseApiController, IAccountBLL
    {
        private IAccountBLL _userBll;


        public UserAccountController()
        {
            _userBll = new AccountBLL();
        }

        [HttpGet]
        public IDataShell<string> Ping(string x)
        {
            return "ok".Succ();
        }

        /// <summary>
        /// 用户.业务层.获取账户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountModel> Get(AccountModel user)
        {
            Console.WriteLine("call business_service/useraccount/get");
            var res = _userBll.Get(user);
            return res;
        }

        /// <summary>
        /// 分页查询用户帐号
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            Console.WriteLine("call business_service/useraccount/getpage");
            var res = _userBll.GetPage(condition);
            return res;
        }

        /// <summary>
        /// 插入一个新用户帐号信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountModel> Insert(AccountModel user)
        {
            var res = _userBll.Insert(user);
            return res;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountModel> Login(AccountModel user)
        {

            var res = _userBll.Login(user);
            return res;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountModel> ModifyPassword(ModifyPasswordReq req)
        {
            var res = _userBll.ModifyPassword(req);
            return res;
        }


        [HttpPost]
        public IDataShell<AccountModel> Regist(AccountModel user)
        {
            var res = _userBll.Regist(user);
            return res;
        }

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
