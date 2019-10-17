using System;
using WL.Account.DataService;
using WL.Account.Core.Business;
using WL.Account.Core.DB;
using WL.Account.Core.DB.Interface;
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

        #region --更新功能--
        public DataShell<AccountDBModel> Update(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/Update";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }

        public DataShell<AccountDBModel> UpdateAfterLogin(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/UpdateAfterLogin";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }

        public DataShell<AccountDBModel> UpdateModifyPassword(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/UpdateModifyPassword";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        #endregion

        #region --获取功能--
        public DataShell<AccountDBModel> GetByAccountID(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByAccountID";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }

        public DataShell<AccountDBModel> GetByAccount(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByAccount";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }

        public DataShell<AccountDBModel> GetByEmail(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByEmail";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }

        public DataShell<AccountDBModel> GetByMobile(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByMobile";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }

        public DataShell<AccountDBModel> GetByCoreID(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByCoreID";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        #endregion
    }

    
}
