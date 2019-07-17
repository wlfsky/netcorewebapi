using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.EasyHttpClient
{
    /// <summary>
    /// httpclient
    /// </summary>
    public interface IEasyHttpClient
    {
        /// <summary>
        /// 异常结果构成器
        /// </summary>
        Func<Exception, string> MakeExceptionResult { get; set; }
        /// <summary>
        /// 设置基本路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IEasyHttpClient SetBaseUrl(string url);
        /// <summary>
        /// 设置头
        /// </summary>
        /// <param name="headerKey"></param>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        IEasyHttpClient SetHeader(string headerKey, string headerValue);
        /// <summary>
        /// post多部分混合数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <param name="form"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<string> PostMultipart(string url, IDictionary<string, string> header, IDictionary<string, string> form = null, IDictionary<string, string> file = null);
        /// <summary>
        /// post 流数据
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="stream"></param>
        /// <param name="buffSize"></param>
        /// <returns></returns>
        Task<string> Post(string uriStr, Stream stream, int buffSize = 2048);
        /// <summary>
        /// post byte数组
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> Post(string uriStr, byte[] data);
        /// <summary>
        /// post 字符串
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> Post(string uriStr, string data);
        /// <summary>
        /// get 字符串
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> Get(string uriStr, string data = "");

        /// <summary>
        /// get byte数组
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<byte[]> GetByte(string uriStr, string data = "");

        /// <summary>
        /// get stream
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<Stream> GetStream(string uriStr, string data = "");

        void GetToFile(string url, string data = "", string filePath = "temp.file", int buffsize = 51200, Action<int, int> downloading = null);
        /// <summary>
        /// put字符串
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> Put(string uriStr, string data = "");
        /// <summary>
        /// delete 字符串
        /// </summary>
        /// <param name="uriStr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> Delete(string uriStr, string data = "");
    }
}
