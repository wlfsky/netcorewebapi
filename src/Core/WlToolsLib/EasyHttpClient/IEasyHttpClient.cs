using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.EasyHttpClient
{
    public interface IEasyHttpClient
    {
        Func<Exception, string> MakeExceptionResult { get; set; }
        IEasyHttpClient SetBaseUrl(string url);
        IEasyHttpClient SetHeader(string headerKey, string headerValue);
        Task<string> PostMultipart(string url, IDictionary<string, string> header, IDictionary<string, string> form = null, IDictionary<string, string> file = null);
        Task<string> Post(string uriStr, Stream stream, int buffSize = 2048);
        Task<string> Post(string uriStr, byte[] data);
        Task<string> Post(string uriStr, string data);
        Task<string> Get(string uriStr, string data = null);
        Task<string> Put(string uriStr, string data = null);
        Task<string> Delete(string uriStr, string data = null);
    }
}
