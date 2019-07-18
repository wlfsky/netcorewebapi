using System;
using WL.Account.DataService;
using WL.Account.Model.Business;
using WL.Account.Model.DB;
using WL.Core.InterfaceBridge.InterfaceBridge;
using WlToolsLib.DataShell;

namespace WL.Account.DataBridge
{
    public class AccountDataBridge : IUserAccountDAL
    {
        IServiceBridge bridge;
        public AccountDataBridge()
        {
            bridge = new WebApiServiceBridge();
            bridge.Version = 1;
            bridge.ServiceUrlMaker = new AccountDataUrlMaker();
        }

        public DataShell<int> Del(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/Del";
            var res = bridge.CallApi<AccountDBModel, DataShell<int>>(funcUrl, user);
            return res;
        }

        public DataShell<AccountDBModel> Get(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/get";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }

        public DataShell<AccountDBModel> Insert(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/Insert";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }

        public DataShell<AccountDBModel> Update(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/Update";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
    }

    
}
