using System;
using WL.Account.DataService;
using WL.Account.Model.Business;
using WL.Account.Model.DB;
using WL.Core.InterfaceBridge.InterfaceBridge;
using WlToolsLib.DataShell;

namespace WL.Account.DataBridge
{
    public class UserAccountBridge : IUserAccountDAL
    {
        IServiceBridge bridge;
        public UserAccountBridge()
        {
            bridge = new WebApiServiceBridge();
        }

        public DataShell<UserAccountDBModel> Get(UserAccountDBModel user)
        {
            var res = bridge.CallApi<UserAccountDBModel, DataShell<UserAccountDBModel>>(user);
            return res;
        }
    }
}
