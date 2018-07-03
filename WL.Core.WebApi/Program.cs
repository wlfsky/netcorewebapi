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

            //Test_Th_Pool();


            BuildWebHost(args).Run();

            
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
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
                .Build();


        public static void Test_Th_Pool()
        {
            #region --线程池测试--
            //
            List<Action> actions = new List<Action>
            {
                ()=>{Console.WriteLine("A-1");},
                ()=>{Console.WriteLine("A-2");},
                ()=>{Console.WriteLine("A-3");},
                ()=>{Console.WriteLine("A-4");}
            };
            foreach (var action in actions)
            {
                var Tempaction = action;
                System.Threading.ThreadPool.QueueUserWorkItem(callBack: ststus => Tempaction());
            }
            //Console.ReadKey();
            #endregion
        }
    }
}
