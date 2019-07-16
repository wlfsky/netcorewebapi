using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    /// <summary>
    /// 默认url构成器样本
    /// </summary>
    public class DefaultUrlMaker : IUrlMaker
    {
        /// <summary>
        /// 基础路径
        /// </summary>
        public string BaseUrl { get; set; } = "http://192.168.1.100";

        /// <summary>
        /// 构成器方法
        /// </summary>
        /// <param name="bridge"></param>
        /// <param name="funcUrl"></param>
        /// <returns></returns>
        public string MakerUrl(IServiceBridge bridge, string funcUrl)
        {
            var urlWithVersion = $"{BaseUrl}/{bridge.Version}{funcUrl}";
            return $"{BaseUrl}{funcUrl}";
        }
    }
}
