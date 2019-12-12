using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WL.Account.Core.Business;
using WL.Account.Core.DB;
using WL.Account.Core.DB.Interface;
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
        public IDataShell<PageShell<AccountDBModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            Console.WriteLine("call data_service/useraccount/get");
            var res = _userDAL.GetPage(condition);
            return res;
        }

        [HttpPost]
        public IDataShell<AccountDBModel> Get(AccountDBModel user)
        {
            Console.WriteLine("call data_service/useraccount/get");
            var res = _userDAL.Get(user);
            return res;
        }

        [HttpPost]
        public IDataShell<AccountDBModel> Insert(AccountDBModel user)
        {
            var res = _userDAL.Insert(user);
            return res;
        }

        [HttpPost]
        public IDataShell<AccountDBModel> Update(AccountDBModel user)
        {
            var res = _userDAL.Update(user);
            return res;
        }

        [HttpPost]
        public IDataShell<int> Del(AccountDBModel user)
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
        public IDataShell<AccountDBModel> UpdateAfterLogin(AccountDBModel user)
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
        public IDataShell<AccountDBModel> UpdateModifyPassword(AccountDBModel user)
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

        public IDataShell<AccountDBModel> GetByAccount(AccountDBModel user)
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
        public IDataShell<AccountDBModel> GetByEmail(AccountDBModel user)
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
        public IDataShell<AccountDBModel> GetByMobile(AccountDBModel user)
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
        public IDataShell<AccountDBModel> GetByAccountID(AccountDBModel user)
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
        public IDataShell<AccountDBModel> GetByCoreID(AccountDBModel user)
        {
            var res = _userDAL.GetByCoreID(user);
            return res;
        }
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountDBModel> UpdateStatus(AccountDBModel user)
        {
            var res =  _userDAL.UpdateStatus(user);
            return res;
        }
        /// <summary>
        /// 设置临时密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountDBModel> SetTempPassword(AccountDBModel user)
        {
            var res = _userDAL.SetTempPassword(user);
            return res;
        }
        /// <summary>
        /// 更新昵称
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountDBModel> UpdateNickName(AccountDBModel user)
        {
            var res = _userDAL.UpdateNickName(user);
            return res;
        }
        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountDBModel> UpdateNickPic(AccountDBModel user)
        {
            var res = _userDAL.UpdateNickPic(user);
            return res;
        }
        /// <summary>
        /// 设置真名
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountDBModel> SetRealName(AccountDBModel user)
        {
            var res = _userDAL.SetRealName(user);
            return res;
        }
        /// <summary>
        /// 设置身份信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountDBModel> SetIDCard(AccountDBModel user)
        {
            var res = _userDAL.SetIDCard(user);
            return res;
        }
        /// <summary>
        /// 更新用户备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountDBModel> UpdateUserRemark(AccountDBModel user)
        {
            var res = _userDAL.UpdateUserRemark(user);
            return res;
        }
        /// <summary>
        /// 更新系统备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IDataShell<AccountDBModel> UpdateSysRemark(AccountDBModel user)
        {
            var res = _userDAL.UpdateSysRemark(user);
            return res;
        }
        /// <summary>
        /// 获取全部用户信息，用于管理端对照提取用户关联信息使用
        /// </summary>
        /// <returns></returns>
        public IDataShell<IEnumerable<AccountDBModel>> GetAllAccount()
        {
            var res = _userDAL.GetAllAccount();
            return res;
        }
        #endregion
    }
}
