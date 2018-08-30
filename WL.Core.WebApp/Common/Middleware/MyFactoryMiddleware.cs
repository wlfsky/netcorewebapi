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

        public MyFactoryMiddleware(string db)
        {
            _db = db;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var keyValue = context.Request.Query["key"];

            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                await context.Response.WriteAsync(_db);
            }

            await next(context);
        }
    }

    public static class MyFactoryMiddlewareExtensions
    {

        public static IApplicationBuilder UseMyFactoryMiddleware(
    this IApplicationBuilder builder, bool option)
        {
            // Passing 'option' as an argument throws a NotSupportedException at runtime.
            return builder.UseMiddleware<MyFactoryMiddleware>(option);
        }
    }
}
