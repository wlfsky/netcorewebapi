using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    public interface IUrlMaker
    {
        string MakerUrl(IServiceBridge bridge, string funcUrl);
    }
}
