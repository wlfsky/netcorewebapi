using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WL.Core.WebApp.Common.Middleware
{
    public class MyFactoryMiddleware : IMiddleware
    {
        private readonly string _db;

        public MyFactoryMiddleware()
        {
            _db = "def val";
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var keyValue = context.Request.Query["key"];

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                await context.Response.WriteAsync(_db+keyValue);
            }

            await next(context);
        }
    }

    public static class MyFactoryMiddlewareExtensions
    {

        public static IApplicationBuilder UseMyFactoryMiddleware(
    this IApplicationBuilder builder, string name)
        {
            // Passing 'option' as an argument throws a NotSupportedException at runtime.
            // 下面这句代码会提示错误，就像上面这句英语说的，传递option会引发NotSupportedException异常
            // return builder.UseMiddleware<MyFactoryMiddleware>(option);
            return builder.UseMiddleware<MyFactoryMiddleware>();
        }
    }
}
