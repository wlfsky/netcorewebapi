using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WlToolsLib.Extension;

namespace WL.Core.WebApp.Common.Middleware
{
    /// <summary>
    /// 定义中间件
    /// </summary>
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var godHeader = context.Request.Query["girl"];//context.Request.Headers["godisagirl"];
            if (godHeader.HasItem())
            {
                Console.WriteLine("God Is Come! and She Is a Girl!");
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }

    /// <summary>
    /// 中间件扩展
    /// </summary>
    public static class MyMiddlewareExtension
    {
        /// <summary>
        /// 注册 MyMiddleware 扩展
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMyMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
