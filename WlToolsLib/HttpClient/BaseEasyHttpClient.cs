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

namespace WlToolsLib.EasyHttpClient
{
    public class BaseEasyHttpClient
    {
        private HttpClient _httpClient;
        public Dictionary<string, string> HeaderDic { get; set; }

        public static readonly Encoding DefEncoding = Encoding.UTF8;

        public static readonly string DefMideaType = "application/json";

        public BaseEasyHttpClient(HttpClient client)
        {
            _httpClient = client;
            if (client.IsNull())
            {
                _httpClient = new HttpClient();
            }
            _httpClient.BaseAddress = new Uri("https://api.github.com/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "");

        }

        public BaseEasyHttpClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.github.com/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "");

        }



        /// <summary>
        /// 提交组合数据，
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <param name="form"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> PostMultipart(string url, IDictionary<string, string> header, IDictionary<string, string> form, IDictionary<string, string> file)
        {
            using (var client = new HttpClient())
            {
                var uriStr = url;
                var uri = new Uri(uriStr);
                client.BaseAddress = uri;
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
                    using (var message = await client.PostAsync(uri, content))
                    {
                        var input = await message.Content.ReadAsStringAsync();
                        return input;
                    }
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
                using (var msg = client.PostAsync(uri, hc).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()))
                {
                    var r = msg.Result.Content.ReadAsStringAsync();
                    return r;
                }
            }
        }

        public Task<string> Get(string uriStr)
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
                using (var msg = client.GetStringAsync(uri).ContinueWith((postTask) => postTask.Result))
                {
                    return msg;
                }
            }
        }

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