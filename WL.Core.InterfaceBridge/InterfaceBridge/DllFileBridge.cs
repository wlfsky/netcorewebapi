using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    public class DllFileBridge : IServiceBridge
    {
        public TRes CallApi<TReq, TRes>(TReq req)
        {
            throw new NotImplementedException();
        }

        public string ReqTrans<TReq>(TReq req)
        {
            throw new NotImplementedException();
        }

        public TRes ResTrans<TRes>(string resStr)
        {
            throw new NotImplementedException();
        }
    }
}
