using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    /// <summary>
    /// 路径构成器
    /// </summary>
    public interface IUrlMaker
    {
        /// <summary>
        /// 基础路径
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// 路径构成器方法
        /// </summary>
        /// <param name="bridge"></param>
        /// <param name="funcUrl"></param>
        /// <returns></returns>
        string MakerUrl(IServiceBridge bridge, string funcUrl);
    }
}
