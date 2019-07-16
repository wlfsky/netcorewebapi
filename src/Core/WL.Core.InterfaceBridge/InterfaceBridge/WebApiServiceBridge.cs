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

        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }

        public WebApiServiceBridge()
        {
            ServiceUrlMaker = new DefaultUrlMaker();
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
            var t = Task.Run(async () => {
                IEasyHttpClient hc = new DefaultEasyHttpClient();
                resStr = await hc.SetBaseUrl(this.ServiceUrlMaker.BaseUrl).Post<TReq>(funcUrl, req);
            });
            Task.WaitAll(t);
            Console.WriteLine($"接口桥返回数据:{resStr}");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcUrl"></param>
        /// <param name="reqStr"></param>
        /// <returns></returns>
        [Obsolete("不再使用，具体功能交给了 wl lib的easyhttpclient")]
        protected async Task<string> CallApi(string funcUrl, string reqStr)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpContent content = new StringContent(reqStr, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(funcUrl, content);
                    //改成自己的 
                    response.EnsureSuccessStatusCode();//用来抛异常的 
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    return e.Message;
                }
            }

            #region --和--
            // zhushi \
            var dd = 100;
            #endregion


            //using (HttpClient client = new HttpClient())
            //{
            //    try
            //    {
            //        HttpResponseMessage response = await client.GetAsync("http://255.255.255.254:5000/api/auth");
            //        response.EnsureSuccessStatusCode();//用来抛异常的 
            //        string responseBody = await response.Content.ReadAsStringAsync();
            //    }
            //    catch (HttpRequestException e)
            //    {
            //        //errorlog
            //    }
            //}
        }
    }
}
