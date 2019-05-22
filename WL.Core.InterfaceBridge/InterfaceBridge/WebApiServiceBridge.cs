using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                resStr = await CallApi(funcUrl, ReqTrans(req));
            });
            Task.WaitAll(t);
            return ResTrans<TRes>(resStr);
        }

        public string ReqTrans<TReq>(TReq req)
        {
            throw new NotImplementedException();
        }

        public TRes ResTrans<TRes>(string reqStr)
        {
            throw new NotImplementedException();
        }

        protected async Task<string> CallApi(string funcUrl, string reqStr)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpContent content = new StringContent(reqStr);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await client.PostAsync(funcUrl, content);
                    //改成自己的 
                    response.EnsureSuccessStatusCode();
                    //用来抛异常的 
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    return e.Message;
                }
            }

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
