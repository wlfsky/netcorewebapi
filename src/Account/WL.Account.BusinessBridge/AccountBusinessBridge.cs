using System;
using WL.Account.Model.Business;
using WL.Account.Model.Business.Interface;
using WL.Core.InterfaceBridge.InterfaceBridge;
using WlToolsLib.DataShell;

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
            string funcUrl = "/UserAccount/get";
            var res = bridge.CallApi<AccountModel, DataShell<AccountModel>>(funcUrl, user);
            return res;
        }
    }

    
}
