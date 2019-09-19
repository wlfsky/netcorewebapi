using System;
using WL.Account.Model.Business;
using WL.Account.Model.Business.Interface;
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

        public DataShell<AccountModel> Get(AccountModel user)
        {
            string funcUrl = "/api/UserAccount/get";
            var res = bridge.CallApi<AccountModel, DataShell<AccountModel>>(funcUrl, user);
            return res;
        }

        public DataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            string funcUrl = "/api/UserAccount/getpage";
            var res = bridge.CallApi<PageCondition<UserQueryPageCondition>, DataShell<PageShell<AccountModel>>>(funcUrl, condition);
            return res;
        }
    }

    
}
