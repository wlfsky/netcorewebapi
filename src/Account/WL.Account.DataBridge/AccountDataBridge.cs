using System;
using WL.Account.DataService;
using WL.Account.Model.Business;
using WL.Account.Model.DB;
using WL.Account.Model.DB.Interface;
using WL.Core.InterfaceBridge.InterfaceBridge;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

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

        public DataShell<PageShell<AccountDBModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            Console.WriteLine("call data_service/useraccount/getpage");
            string funcUrl = "/api/UserAccount/GetPage";
            var res = bridge.CallApi<PageCondition<UserQueryPageCondition>, DataShell<PageShell<AccountDBModel>>>(funcUrl, condition);
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
