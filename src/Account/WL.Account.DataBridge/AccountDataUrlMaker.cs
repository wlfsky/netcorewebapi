using System;
using System.Collections.Generic;
using System.Text;
using WL.Core.InterfaceBridge.InterfaceBridge;

namespace WL.Account.DataBridge
{
    public class AccountDataUrlMaker : IUrlMaker
    {
        public string BaseUrl { get; set; } = "http://localhost:9011";
        public string MakerUrl(IServiceBridge bridge, string funcUrl)
        {
            return $"{this.BaseUrl}{funcUrl}";
        }
    }
}
