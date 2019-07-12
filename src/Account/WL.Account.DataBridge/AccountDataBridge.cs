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

        public DataShell<AccountDBModel> Get(AccountDBModel user)
        {
            string funcUrl = "/UserAccount/get";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
    }

    public class AccountDataUrlMaker : IUrlMaker
    {
        public string MakerUrl(IServiceBridge bridge, string funcUrl)
        {
            return $"http://localhost:9011/api{funcUrl}";
        }
    }
}
