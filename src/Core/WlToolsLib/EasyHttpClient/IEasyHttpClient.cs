using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.EasyHttpClient
{
    public interface IEasyHttpClient
    {
        IEasyHttpClient SetBaseUrl(string url);
        IEasyHttpClient SetHeader(string headerKey, string headerValue);
        Task<string> PostMultipart(string url, IDictionary<string, string> header, IDictionary<string, string> form = null, IDictionary<string, string> file = null);
        Task<string> Post<T>(string uriStr, T obj);
        Task<string> Get(string uriStr);
        Task<string> Put<TIn>(string uriStr, TIn obj);
        Task<string> Delete(string uriStr);
    }
}
