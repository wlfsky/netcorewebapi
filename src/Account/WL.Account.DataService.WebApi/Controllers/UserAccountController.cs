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

        #region --更新--
        /// <summary>
        /// 登录后更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateAfterLogin(AccountDBModel user)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 修改密码更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateModifyPassword(AccountDBModel user)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --查询--
        /// <summary>
        /// 根据帐号获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public DataShell<AccountDBModel> GetByAccount(AccountDBModel user)
        {
            return _userDAL.GetByAccount(user);
        }
        /// <summary>
        /// 根据邮件获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByEmail(AccountDBModel user)
        {
            return _userDAL.GetByEmail(user);
        }
        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByMobile(AccountDBModel user)
        {
            return _userDAL.GetByMobile(user);
        }
        /// <summary>
        /// 根据用户帐号id获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByAccountID(AccountDBModel user)
        {
            return _userDAL.GetByAccountID(user);
        }
        /// <summary>
        /// 根据核心id获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByCoreID(AccountDBModel user)
        {
            return _userDAL.GetByCoreID(user);
        }
        #endregion
    }
}
