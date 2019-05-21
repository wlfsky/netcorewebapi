using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.InterfaceBridge.InterfaceBridge
{
    public class WebApiServiceBridge : IServiceBridge
    {
        public TRes CallApi<TReq, TRes>(TReq req)
        {
            string resStr = "";
            var t = Task.Run(async () => {
                resStr = await CallApi(ReqTrans(req));
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

        protected async Task<string> CallApi(string reqStr)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpContent content = new StringContent(reqStr);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await client.PostAsync("http://255.255.255.254:5000/api/auth", content);
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
