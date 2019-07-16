using System;
using System.Collections.Generic;
using System.Text;
using WL.Core.InterfaceBridge.InterfaceBridge;

namespace WL.Account.BusinessBridge
{
    /// <summary>
    /// 帐号.业务层.url生成器
    /// 当前简写
    /// </summary>
    public class AccountBusinessUrlMaker : IUrlMaker
    {
        public string BaseUrl { get; set; } = "http://localhost:9021";

        // 要素：地址，版本 
        public string MakerUrl(IServiceBridge bridge, string funcUrl)
        {
            return $"{this.BaseUrl}{funcUrl}";
        }
    }
}
