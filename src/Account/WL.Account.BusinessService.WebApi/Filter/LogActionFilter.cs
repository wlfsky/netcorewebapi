using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace WL.Account.BusinessService.WebApi.Filter
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            using (MemoryStream sm = new System.IO.MemoryStream())
            {
                context.HttpContext.Response.Body.CopyTo(sm);
                Span<byte> resBytes = new Span<byte>();
                var resNum = sm.Read(resBytes);
                var resStr = Encoding.UTF8.GetString(resBytes.ToArray());
                Console.WriteLine($"Response -->");
                Console.WriteLine(resStr);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            using (MemoryStream sm = new System.IO.MemoryStream())
            {
                context.HttpContext.Request.Body.CopyTo(sm);
                Span<byte> reqBytes = new Span<byte>();
                var reqNum = sm.Read(reqBytes);
                var reqStr = Encoding.UTF8.GetString(reqBytes.ToArray());
                Console.WriteLine($"Request -->");
                Console.WriteLine(reqStr);
            }
        }
    }
}
