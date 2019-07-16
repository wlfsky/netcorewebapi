using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WlToolsLib.Extension;
using System.Globalization;
using static System.Console;
using System.Text;
using WlToolsLib.JsonHelper;
using System.Net.Http.Headers;
using WlToolsLib.DataShell;

namespace WlToolsLib.EasyHttpClient
{
    /// <summary>
    /// 简单http客户端
    /// </summary>
    public class DefaultEasyHttpClient : IEasyHttpClient
    {
        /// <summary>
        /// http客户端
        /// </summary>
        private HttpClient _defaultHttpClient;

        /// <summary>
        /// http头
        /// </summary>
        public Dictionary<string, string> HeaderDic { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 基础 路径
        /// </summary>
        public string BaseUrl { get; set; }
        
        /// <summary>
        /// 编码
        /// </summary>
        public static readonly Encoding DefEncoding = Encoding.UTF8;

        /// <summary>
        /// 默认媒体类型
        /// </summary>
        public static readonly string DefMideaType = "application/json";

        /// <summary>
        /// 给定http客户端，初始化
        /// </summary>
        /// <param name="client"></param>
        public DefaultEasyHttpClient(HttpClient client)
        {
            _defaultHttpClient = client;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public DefaultEasyHttpClient()
        {
            _defaultHttpClient = new HttpClient();
        }

        protected Uri MakeFullUri(string url)
        {
            return new Uri($"{this.BaseUrl}{url}");
        }

        protected string MakeError(Exception ex)
        {

            return $"Message:{ex.Message} StackTrace:{ex.StackTrace}";
        }

        protected string MakeError<TRes>(Exception ex)
        {
            var err_res = DataShellCreator.CreateFail<TRes>(ex);
            return err_res.ToJson();
        }

        /// <summary>
        /// 设置基本路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public IEasyHttpClient SetBaseUrl(string baseUrl)
        {
            if (baseUrl.NullEmpty())
            {
                throw new Exception("no base url");
            }
            this.BaseUrl = baseUrl;
            //_defaultHttpClient.BaseAddress = new Uri(baseUrl);
            return this;
        }

        /// <summary>
        /// 设置http头
        /// </summary>
        /// <param name="headerKey"></param>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        public IEasyHttpClient SetHeader(string headerKey, string headerValue)
        {
            if(!HeaderDic.Any((h) => h.Key.ToLower() == headerKey.ToLower() ))
            {
                HeaderDic.Add(headerKey, headerValue);
            }
            return this;
        }

        /// <summary>
        /// 提交组合数据，
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <param name="form"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task<string> PostMultipart(string url, IDictionary<string, string> header, IDictionary<string, string> form = null, IDictionary<string, string> file = null)
        {
            using (var client = new HttpClient())
            {
                // 这里要重写
                var uri = MakeFullUri(url);
                using (var content = new MultipartFormDataContent("----Upload-WlClient----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    if (header.HasItem())
                    {
                        foreach (var item in header)
                        {
                            content.AddHeaderContent(item.Key, item.Value);
                        }
                    }
                    if (form.HasItem())
                    {
                        foreach (var item in form)
                        {
                            content.AddFormContent(item.Key, item.Value);
                        }
                    }
                    if (file.HasItem())
                    {
                        foreach (var item in file)
                        {
                            content.AddFileContent(item.Key, item.Value);
                        }
                    }
                    return client.PostAsync(uri, content).Result.Content.ReadAsStringAsync();
                }
            }
        }

        /// <summary>
        /// Post发送json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uriStr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task<string> Post<T>(string uriStr, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = MakeFullUri(uriStr);
                    var reqJsonStr = obj.ToJson();
                    HttpContent hc = new StringContent(reqJsonStr, DefEncoding, DefMideaType);
                    return client.PostAsync(uri, hc).Result.Content.ReadAsStringAsync();
                   
                }
                catch (Exception ex)
                {
                    // 这里要用task.run来返回错误信息。
                    // 只用new Task只返回任务没有实际执行，会一直等待
                    return Task.Run<string>(() => { return MakeError<T>(ex); });
                }
            }
        }

        /// <summary>
        /// get动作访问路径
        /// </summary>
        /// <param name="uriStr"></param>
        /// <returns></returns>
        public Task<string> Get(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = MakeFullUri(url);
                    return client.GetAsync(uri).Result.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return Task.Run<string>(() => { return MakeError(ex); });
                }
            }
        }

        /// <summary>
        /// put动作提交
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="uriStr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task<string> Put<TIn>(string uriStr, TIn obj)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri(uriStr);
                client.BaseAddress = uri;
                if (HeaderDic.HasItem())
                {
                    foreach (var item in HeaderDic)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                var reqJsonStr = obj.ToJson();
                HttpContent hc = new StringContent(reqJsonStr, DefEncoding, DefMideaType);
                using (var msg = client.PutAsync(uri, hc).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()))
                {
                    var r = msg.Result.Content.ReadAsStringAsync();
                    return r;
                }
            }
        }

        /// <summary>
        /// delete动作提交
        /// </summary>
        /// <param name="uriStr"></param>
        /// <returns></returns>
        public Task<string> Delete(string uriStr)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri(uriStr);
                client.BaseAddress = uri;
                if (HeaderDic.HasItem())
                {
                    foreach (var item in HeaderDic)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                using (var msg = client.DeleteAsync(uri).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()))
                {
                    var r = msg.Result.Content.ReadAsStringAsync();
                    return r;
                }
            }
        }
    }
}