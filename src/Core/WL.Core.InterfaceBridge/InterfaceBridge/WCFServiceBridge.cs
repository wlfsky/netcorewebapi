using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    /// <summary>
    /// WCF 服务桥
    /// </summary>
    public class WCFServiceBridge : IServiceBridge
    {
        /// <summary>
        /// 服务路径构造器
        /// </summary>
        public IUrlMaker ServiceUrlMaker { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 访问api主程序
        /// </summary>
        /// <typeparam name="TReq"></typeparam>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public TRes CallApi<TReq, TRes>(TReq req)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 带地址的访问方法
        /// </summary>
        /// <typeparam name="TReq"></typeparam>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public TRes CallApi<TReq, TRes>(string url, TReq req)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 请求转换
        /// </summary>
        /// <typeparam name="TReq"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public string ReqTrans<TReq>(TReq req)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 相应转换
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="resStr"></param>
        /// <returns></returns>
        public TRes ResTrans<TRes>(string resStr)
        {
            throw new NotImplementedException();
        }
    }
}
