using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    public class WCFServiceBridge : IServiceBridge
    {
        public IUrlMaker ServiceUrlMaker { get; set; }
        public int Version { get; set; }

        public TRes CallApi<TReq, TRes>(TReq req)
        {
            throw new NotImplementedException();
        }

        public TRes CallApi<TReq, TRes>(string url, TReq req)
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
