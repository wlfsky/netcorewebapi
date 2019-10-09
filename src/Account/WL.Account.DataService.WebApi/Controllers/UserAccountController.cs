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
            var res = _userDAL.Del(user);
            return res;
        }

        #region --更新--
        /// <summary>
        /// 登录后更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public DataShell<AccountDBModel> UpdateAfterLogin(AccountDBModel user)
        {
            var res = _userDAL.UpdateAfterLogin(user);
            return res;
        }
        /// <summary>
        /// 修改密码更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public DataShell<AccountDBModel> UpdateModifyPassword(AccountDBModel user)
        {
            var res = _userDAL.UpdateModifyPassword(user);
            return res;
        }
        #endregion

        #region --查询--
        /// <summary>
        /// 根据帐号获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]

        public DataShell<AccountDBModel> GetByAccount(AccountDBModel user)
        {
            var res = _userDAL.GetByAccount(user);
            return res;
        }
        /// <summary>
        /// 根据邮件获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public DataShell<AccountDBModel> GetByEmail(AccountDBModel user)
        {
            var res = _userDAL.GetByEmail(user);
            return res;
        }
        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public DataShell<AccountDBModel> GetByMobile(AccountDBModel user)
        {
            var res = _userDAL.GetByMobile(user);
            return res;
        }
        /// <summary>
        /// 根据用户帐号id获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public DataShell<AccountDBModel> GetByAccountID(AccountDBModel user)
        {
            var res = _userDAL.GetByAccountID(user);
            return res;
        }
        /// <summary>
        /// 根据核心id获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public DataShell<AccountDBModel> GetByCoreID(AccountDBModel user)
        {
            var res = _userDAL.GetByCoreID(user);
            return res;
        }
        #endregion
    }
}
