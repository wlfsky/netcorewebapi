using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.Extension;
using WlToolsLib.JsonHelper;

namespace WL.Core.Common.Mvc
{
    public class ApiLogActionFilter : IActionFilter
    {
        private DateTime? startTime;
        private DateTime? endTime;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Task.Run(() =>
            {
                var resJson = context.Result.ToJson();
                Console.WriteLine($"{context.HttpContext.Response.ContentType} Response -->");
                Console.WriteLine(resJson);
            });
            endTime = DateTime.Now;
            if (startTime.HasValue)
            {
                Console.WriteLine($"{context.RouteData.Values.ToJson()} 耗时：{(endTime - startTime).Value.TotalMilliseconds} 毫秒");
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            startTime = DateTime.Now;
            var req = context.HttpContext.Request;
            Parallel.Invoke(() =>
            {
                if (req.Query.Keys.HasItem())
                {
                    var reqQueryStr = req.Query.ToJson();
                    Console.WriteLine($"{req.Method} Query -->\r{reqQueryStr}");
                }
                if (req.Headers.HasItem())
                {
                    var reqHeaderStr = req.Headers.ToJson();
                    Console.WriteLine($"{req.Method} Header -->\r{reqHeaderStr}");
                }
            }, () =>
            {
                if (req.Body.CanSeek)
                {
                    using (MemoryStream sm = new System.IO.MemoryStream())
                    {
                        var reqStream = context.HttpContext.Request.Body;
                        reqStream.Position = 0;
                        reqStream.CopyTo(sm);
                        reqStream.Position = 0;
                        sm.Position = 0;
                        var reqBytes = sm.ToArray();
                        var reqStr = Encoding.UTF8.GetString(reqBytes);
                        Console.WriteLine($"{req.Method} Request -->");
                        Console.WriteLine(reqStr);
                    }
                }
            });

        }
    }
}
