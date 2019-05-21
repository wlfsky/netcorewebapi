using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    public interface IServiceBridge
    {
        string ReqTrans<TReq>(TReq req);
        TRes ResTrans<TRes>(string resStr);
        TRes CallApi<TReq, TRes>(TReq req);
    }
}
