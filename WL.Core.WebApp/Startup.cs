using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WL.Core.BusinessService;
using WL.Core.WebApp.Common.Middleware;
using WlToolsLib.Expand;

namespace WL.Core.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 用此方法向容器内加入服务
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <remarks>This method gets called by the runtime. Use this method to add services to the container.</remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            #region --服务注册--
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();
            //services.AddTransient<FactoryActivatedMiddleware>();//有错
            #endregion

            #region --注入业务层  业务服务--
            services.AddSingleton<IUserSystemBLL, UserSystemBLL>();
            #endregion

            services.AddMvc();

            
        }

        /// <summary>
        /// 这个方法被运行时调用，用此方法配置http请求管道
        /// </summary>
        /// <param name="app">应用构成器</param>
        /// <param name="env"></param>
        /// <remarks>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</remarks>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//开发模式显示错误页面
                app.UseBrowserLink();// 浏览连接
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");//非开发模式显示标准错误信息页面
            }

            #region --自定义中间件--
            app.UseMyMiddleware();//使用自定义的中间件
            app.UseMyFactoryMiddleware(true);// 使用自定义的工厂中间件
            #endregion

            #region --自定义中间件 (注意：Use【运行后可控制next】 和 Run【运行后不再运行】)--
            /*
             * 使用 Use、Run 和 Map 配置 HTTP 管道。
             * Use 方法可使管道短路（即不调用 next 请求委托）。
             * Run 是一种约定，并且某些中间件组件可公开在管道末尾运行的 Run[Middleware] 方法。
             */

            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await context.Response.WriteAsync("Hello, World! This is my world! Enjoy!");
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });
            app.Use(next =>
            {
                Console.WriteLine("A\n");
                return async (context) =>
                {
                    // 1. 对Request做一些处理
                    // TODO

                    // 2. 调用下一个中间件
                    Console.WriteLine("A-BeginNext\n");
                    await next(context);
                    Console.WriteLine("A-EndNext\n");

                    // 3. 生成 Response
                    //TODO
                };
            });

            #region --Map 单段路由匹配--
            // 局部函数 1
            void HandleMapTest1(IApplicationBuilder app1)
            {
                app1.Run(async context =>
                {
                    await context.Response.WriteAsync("Map Test 1");
                });
            }
            // 局部函数 2
            void HandleMapTest2(IApplicationBuilder app1)
            {
                app1.Run(async context =>
                {
                    await context.Response.WriteAsync("Map Test 2");
                });
            }
            // map1 局部函数1
            app.Map("/map1", HandleMapTest1);
            // map2 局部函数2
            app.Map("/map2", HandleMapTest2);
            #endregion

            #region --MapWhen 会检查特定条件，匹配则路由到指定处理方法--
            void HandleBranch(IApplicationBuilder app2)
            {
                app2.Run(async context =>
                {
                    var branchVer = context.Request.Query["branch"];
                    await context.Response.WriteAsync($"Branch used = {branchVer}");
                });
            }
            app.MapWhen(context => context.Request.Query.ContainsKey("branch"),
                               HandleBranch);
            #endregion

            #region --Map 嵌套应用 可连续处理 路径--
            app.Map("/lv1", level1App => {
                level1App.Map("/lv2a", level2AApp => {
                    // "/lv1/lv2a" processing
                    level2AApp.Run(async context =>
                    {
                        await context.Response.WriteAsync("/lv1/lv2a processing");
                    });
                });
                level1App.Map("/lv2b", level2BApp => {
                    // "/lv1/lv2b" processing
                    level2BApp.Run(async context =>
                    {
                        await context.Response.WriteAsync("/lv1/lv2b processing");
                    });
                });
            });
            #endregion

            #region --Map 还可同时匹配多个段--
            void HandleMultiSeg(IApplicationBuilder app3)
            {
                app3.Run(async context =>
                {
                    await context.Response.WriteAsync("Map multiple segments.");
                });
            }
            app.Map("/map1/seg1", HandleMultiSeg);
            #endregion

            #region --Run 不向下传递--
            app.Run(async context =>
                {
                // Run 不向下传递
                await context.Response.WriteAsync("Hello, World! This is my world! Enjoy!");
                    // 此代码执行后，未向下继续执行。
                });
            #endregion
            #endregion

            app.UseStaticFiles();// 静态文件

            app.UseAuthentication();// 用户验证

            #region --路由配置--
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion
        }
    }
}
