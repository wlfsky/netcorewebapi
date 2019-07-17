using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WlToolsLib.EasyHttpClient;
using WlToolsLib.Extension;
using WlToolsLib.JsonHelper;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    public class WebApiServiceBridge : IServiceBridge
    {
        /// <summary>
        /// 服务地址生成器
        /// </summary>
        public IUrlMaker ServiceUrlMaker { get; set; }

        public Action<string> Log { get; set; } = (s) => Console.WriteLine(s);

        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }

        private Func<Exception, string> MakeExceptionResult { get; set; } = (ex) =>
         {
             var err_res = DataShellCreator.CreateFail<object>(ex);
             return err_res.ToJson();
         };

        public WebApiServiceBridge()
        {
            ServiceUrlMaker = new DefaultUrlMaker();
        }

        public IServiceBridge SetUrlMaker(IUrlMaker urlMaker)
        {
            this.ServiceUrlMaker = urlMaker;
            return this;
        }
        
        /// <summary>
        /// 呼叫api
        /// </summary>
        /// <typeparam name="TReq"></typeparam>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="funcUrl"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public TRes CallApi<TReq, TRes>(string funcUrl, TReq req)
        {
            string resStr = string.Empty;

            var fullUrl = ServiceUrlMaker.MakerUrl(this, funcUrl);
            var reqjson = req.ToJson();
            var logstr = $"URL: {fullUrl}, REQUEST: {reqjson}";
            Log(logstr);
            var t = Task.Run(async () => {
                IEasyHttpClient hc = new DefaultEasyHttpClient();
                hc.MakeExceptionResult = (ex) =>
                {
                    var err_res = DataShellCreator.CreateFail<TRes>(ex);
                    return err_res.ToJson();
                };
                resStr = await hc.SetBaseUrl(this.ServiceUrlMaker.BaseUrl).Post(funcUrl, reqjson);
            });
            Task.WaitAll(t);
            Log($"接口桥返回数据:{resStr}");
            return ResTrans<TRes>(resStr);
        }

        public string ReqTrans<TReq>(TReq req)
        {
            return req.ToJson();
        }

        public TRes ResTrans<TRes>(string resStr)
        {
            if (resStr.NullEmpty())
            {
                resStr = DataShellCreator.CreateFail<object>("响应结果为空").ToJson();
            }
            return resStr.ToObj<TRes>();
        }

    }
}
