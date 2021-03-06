﻿using System;
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
using System.IO;
using WlToolsLib.LogHelper;

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
        /// 日志
        /// </summary>
        public Action<string> ErrorLog { get; set; } = s => $"ERROR ==> {s}".SendToErrorLog();

        public void ErrLog(Exception ex) => ex.SendToErrorLog();
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

        //protected string MakeExceptionResult(Exception ex)
        //{
        //    var err_res = DataShellCreator.CreateFail<object>(ex);
        //    return err_res.ToJson(ignoreFields: new[] { "Data" });
        //}

        public Func<Exception, string> MakeExceptionResult { get; set; } = (ex) =>
         {
             var err_res = DataShellCreator.CreateFail<Object>(ex);
             return err_res.ToJson();
         };

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
                string tempstr = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                using (var content = new MultipartFormDataContent($"----Upload-WlClient-{tempstr}----"))
                {
                    try
                    {
                        //if (header.HasItem())
                        //{
                        //    foreach (var item in header)
                        //    {
                        //        content.AddHeaderContent(item.Key, item.Value);
                        //    }
                        //}
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
                    catch (Exception ex)
                    {
                        return Task.Run<string>(() => { return MakeExceptionResult(ex); });
                    }
                }
            }
        }

        /// <summary>
        /// post字符串数据
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<string> Post(string uriStr, string data = "")
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = MakeFullUri(uriStr);
                    HttpContent hc = new StringContent(data, DefEncoding, DefMideaType);
                    return client.PostAsync(uri, hc).Result.Content.ReadAsStringAsync();

                }
                catch (Exception ex)
                {
                    ErrLog(ex);
                    // 这里要用task.run来返回错误信息。
                    // 只用new Task只返回任务没有实际执行，会一直等待
                    return Task.Run<string>(() => { return MakeExceptionResult(ex); });
                }
            }
        }

        /// <summary>
        /// post数据流
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="stream"></param>
        /// <param name="buffSize"></param>
        /// <returns></returns>
        public Task<string> Post(string uriStr, Stream stream, int buffSize = 2048)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = MakeFullUri(uriStr);
                    HttpContent hc = new StreamContent(stream, buffSize);
                    return client.PostAsync(uri, hc).Result.Content.ReadAsStringAsync();

                }
                catch (Exception ex)
                {
                    // 这里要用task.run来返回错误信息。
                    // 只用new Task只返回任务没有实际执行，会一直等待
                    return Task.Run<string>(() => { return MakeExceptionResult(ex); });
                }
            }
        }

        /// <summary>
        /// 数据流
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<string> Post(string uriStr, byte[] data)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = MakeFullUri(uriStr);
                    HttpContent hc = new ByteArrayContent(data);
                    return client.PostAsync(uri, hc).Result.Content.ReadAsStringAsync();

                }
                catch (Exception ex)
                {
                    // 这里要用task.run来返回错误信息。
                    // 只用new Task只返回任务没有实际执行，会一直等待
                    return Task.Run<string>(() => { return MakeExceptionResult(ex); });
                }
            }
        }

        /// <summary>
        /// get动作访问路径
        /// </summary>
        /// <param name="uriStr"></param>
        /// <returns></returns>
        public Task<string> Get(string url, string data = "")
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = MakeFullUri(url);
                    HttpRequestMessage req = new HttpRequestMessage(new HttpMethod("GET"), uri);
                    req.Content = new StringContent(data);
                    return client.SendAsync(req).Result.Content.ReadAsStringAsync();
                    //return client.GetAsync(uri).Result.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return Task.Run<string>(() => { return MakeExceptionResult(ex); });
                }
            }
        }

        /// <summary>
        /// get byte数组
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<byte[]> GetByte(string url, string data = "")
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = MakeFullUri(url);
                    HttpRequestMessage req = new HttpRequestMessage(new HttpMethod("GET"), uri);
                    req.Content = new StringContent(data);
                    return client.SendAsync(req).Result.Content.ReadAsByteArrayAsync();
                    //return client.GetAsync(uri).Result.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return Task.Run<byte[]>(() => { MakeExceptionResult(ex); return new byte[1] { 0 }; });
                }
            }
        }

        /// <summary>
        /// get stream
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<Stream> GetStream(string url, string data = "")
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = MakeFullUri(url);
                    HttpRequestMessage req = new HttpRequestMessage(new HttpMethod("GET"), uri);
                    req.Content = new StringContent(data);
                    return client.SendAsync(req).Result.Content.ReadAsStreamAsync();
                    //return client.GetAsync(uri).Result.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return Task.Run<Stream>(() => { MakeExceptionResult(ex); return new MemoryStream(); });
                }
            }
        }

        /// <summary>
        /// Get方式获取文件，支持超大文件
        /// </summary>
        /// <param name="url">GET路径</param>
        /// <param name="data">get时带入body的数据</param>
        /// <param name="buffsize">缓存尺寸，默认51200</param>
        /// <param name="downloading">下载中事件</param>
        /// <param name="beforGet">Get前</param>
        /// <param name="beforReadStream">读取流前</param>
        /// <param name="setFilePath">设置文件名</param>
        /// <param name="afterGet">Get后，销毁response前</param>
        /// <param name="onException">异常处理，返回true抛出异常，返回false不抛异常</param>
        public async void GetToFile(string url, string data = "", int buffsize = 51200, 
            Action<DateTime, int, int> downloading = null, 
            Action<HttpRequestMessage> beforGet = null, 
            Action<HttpResponseMessage> beforReadStream = null, 
            Func<HttpResponseMessage, string> setFilePath = null, 
            Action<HttpResponseMessage> afterGet = null, 
            Func<Exception, bool> onException = null)
        {
            int intervals = 200; // downloading执行间隔
            int readsize = 1; // 每次读取到的尺寸
            int totalSize = 0; // 总读取尺寸
            int totalReadTimes = 0; // 总读取次数
            DateTime downloadStart = DateTime.Now;

            #region --默认的downloading委托--
            int currReport = 0, reportSize = 5;//这里reportSize实际上以每200次标准读取循环为基数，5就等于 5*200（见intervals）
            Action<DateTime, int, int> defaultDownloading = (stm, trt, ts) =>
                {
                    currReport++;
                    if (currReport == reportSize)
                    {
                        var tms = (DateTime.Now - stm).TotalSeconds;
                        var averageVelocity = (ts / Math.Ceiling(tms)) / 1000000;
                        WriteLine($"TOTAL_SECONDS:{tms} TOTAL_SIZE:{ts} TOTAL_TIMES:{trt} AVERAGE_VELOCITY:{averageVelocity:f2}Mb/S");
                        currReport = 0;
                    }
                };
            if (downloading == null)
            {
                downloading = defaultDownloading;
            }
            else // 如果不是空就在委托尾部加上此委托（多播委托）
            {
                downloading += defaultDownloading;
            }
            #endregion

            #region --默认的设置文件路径方法--
            if (setFilePath == null)
            {
                setFilePath = (resp) =>
                {
                    // 服务端带文件名 就用文件名，不带就用自定义
                    if (resp.Content.Headers.ContentDisposition.FileName.NullEmpty())
                    {
                        return $"datafile.{DateTime.Now.DateTimeID()}.tmp";
                    }
                    else
                    {
                        return resp.Content.Headers.ContentDisposition.FileName;
                    }
                };
            }
            #endregion

            #region --其他默认--
            if(beforReadStream == null)
            {
                beforReadStream = (resp) => {
                    WriteLine($"ContentType:{resp.Content.Headers.ContentType},ContentLength:{resp.Content.Headers.ContentLength},FileName:{resp.Content.Headers.ContentDisposition.FileName}");
                };
            }
            if (beforGet == null)
            {
                beforGet = (q) => { };
            }
            if(afterGet == null)
            {
                afterGet = (s) =>
                {
                    var total_seconds = (DateTime.Now - downloadStart).TotalSeconds;
                    var averageVelocity = (totalSize / Math.Ceiling(total_seconds)) / 1000000;
                    WriteLine($"TOTAL_SECONDS:{total_seconds} TOTAL_SIZE:{totalSize} TOTAL_TIMES:{totalReadTimes} AVERAGE_VELOCITY:{averageVelocity:f2}Mb/S DOWNLOAD END");
                };
            }
            if(onException == null)
            {
                onException = (ex) => true;
            }
            #endregion
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var uri = MakeFullUri(url);
                    HttpRequestMessage req = new HttpRequestMessage(new HttpMethod("GET"), uri);
                    req.Content = new StringContent(data);
                    beforGet(req);
                    using (var res = client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead).Result)
                    {
                        beforReadStream(res);
                        string filePath = setFilePath(res);
                        downloadStart = DateTime.Now;
                        using (Stream contentStream = await res.Content.ReadAsStreamAsync())
                        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                        {
                            byte[] buff = new byte[buffsize];
                            while (readsize > 0)
                            {
                                readsize = contentStream.Read(buff, 0, buffsize);
                                totalSize += readsize;
                                //contentStream.Flush();
                                fs.Write(buff, 0, readsize);
                                //fs.Flush();
                                totalReadTimes++;
                                // 符合间隔数才执行downloading方法
                                if ((totalReadTimes % intervals) == 0)
                                {
                                    fs.Flush();
                                    downloading(downloadStart, totalReadTimes, totalSize);
                                }
                            }
                        }
                        afterGet(res);
                    }
                    //return client.GetAsync(uri).Result.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    if (onException(ex))
                    {
                        throw ex;
                    }
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
        public Task<string> Put(string uriStr, string data = "")
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri(uriStr);
                client.BaseAddress = uri;
                var reqJsonStr = data;
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
        public Task<string> Delete(string uriStr, string data = "")
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