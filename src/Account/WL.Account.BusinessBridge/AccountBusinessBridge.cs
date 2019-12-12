using System;
using WL.Account.Core.Business;
using WL.Account.Core.Business.Interface;
using WL.Account.Core.Core;
using WL.Core.InterfaceBridge.InterfaceBridge;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.BusinessBridge
{
    /// <summary>
    /// 帐号.业务层.桥
    /// </summary>
    public class AccountBusinessBridge : IAccountBLL
    {
        IServiceBridge bridge;
        public AccountBusinessBridge()
        {
            bridge = new WebApiServiceBridge();
            bridge.Version = 1;
            bridge.ServiceUrlMaker = new AccountBusinessUrlMaker();
        }

        /// <summary>
        /// 获取单个用户帐号信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> Get(AccountModel user)
        {
            string funcUrl = "/api/UserAccount/get";
            var res = bridge.CallApi<AccountModel, DataShell<AccountModel>>(funcUrl, user);
            return res;
        }

        /// <summary>
        /// 分页获取获取用户帐号
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IDataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            string funcUrl = "/api/UserAccount/getpage";
            var res = bridge.CallApi<PageCondition<UserQueryPageCondition>, DataShell<PageShell<AccountModel>>>(funcUrl, condition);
            return res;
        }

        /// <summary>
        /// 插入一个新帐号记录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> Insert(AccountModel user)
        {
            string funcUrl = "/api/UserAccount/Insert";
            var res = bridge.CallApi<AccountModel, DataShell<AccountModel>>(funcUrl, user);
            return res;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> Regist(AccountModel user)
        {
            string funcUrl = "/api/UserAccount/Regist";
            var res = bridge.CallApi<AccountModel, DataShell<AccountModel>>(funcUrl, user);
            return res;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> Login(AccountModel user)
        {
            string funcUrl = "/api/UserAccount/Login";
            var res = bridge.CallApi<AccountModel, DataShell<AccountModel>>(funcUrl, user);
            return res;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> ModifyPassword(ModifyPasswordReq req)
        {
            string funcUrl = "/api/UserAccount/ModifyPassword";
            var res = bridge.CallApi<ModifyPasswordReq, DataShell<AccountModel>>(funcUrl, req);
            return res;
        }
    }

    
}
