using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    public interface IServiceBridge
    {
        /// <summary>
        /// 服务地址生成器
        /// </summary>
        IUrlMaker ServiceUrlMaker { get; set; }

        /// <summary>
        /// 接口版本
        /// </summary>
        int Version { get; set; }
        /// <summary>
        /// 请求转换
        /// </summary>
        /// <typeparam name="TReq"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        string ReqTrans<TReq>(TReq req);
        /// <summary>
        /// 相应转换
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="resStr"></param>
        /// <returns></returns>
        TRes ResTrans<TRes>(string resStr);
        /// <summary>
        /// 呼叫api
        /// </summary>
        /// <typeparam name="TReq"></typeparam>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        TRes CallApi<TReq, TRes>(string url, TReq req);
    }
}
