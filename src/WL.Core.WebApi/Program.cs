using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WL.Core.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:9031")
                .UseKestrel(options =>
                    {
                        options.Limits.MaxRequestBufferSize = 512 * 1_000_000;
                        options.Limits.MaxRequestBodySize = 512 * 1_100_000;
                        //options.Listen(IPAddress.Loopback, 5000);
                        //options.Listen(IPAddress.Loopback, 5001, listenOptions =>
                        //{
                        //    listenOptions.UseHttps("testCert.pfx", "testPassword");
                        //});
                    })
        #region --这里是配置日志功能的一种方式，另一种在startup.cs中--
                //.ConfigureLogging((hostingContext, logging) =>
                //    {
                //        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                //        logging.AddConsole();
                //        logging.AddDebug();
                //    }) 
        #endregion
                .Build();

    }
}
