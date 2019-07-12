using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    public class DefaultUrlMaker : IUrlMaker
    {
        string BaseUrl = "192.168.1.100";
        public string MakerUrl(IServiceBridge bridge, string funcUrl)
        {
            return $"http://{bridge.Version}.{BaseUrl}/api{funcUrl}";
        }
    }
}
