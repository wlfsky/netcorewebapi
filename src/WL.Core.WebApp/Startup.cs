using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using WL.Account.BusinessService;
using WL.Account.Model.Business.Interface;
using WL.Core.BusinessService;
using WL.Core.WebApp.Common.Middleware;
using WlToolsLib.Extension;

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
            services.AddTransient<MyFactoryMiddleware>();//注册MyFactoryMiddleware服务
            #endregion

            #region --注入业务层  业务服务--
            services.AddSingleton<IAccountBLL, AccountBLL>();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest); ;

            
        }

        /// <summary>
        /// 这个方法被运行时调用，用此方法配置http请求管道
        /// </summary>
        /// <param name="app">应用构成器</param>
        /// <param name="env"></param>
        /// <remarks>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</remarks>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();//开发模式显示错误页面
            //    //app.UseBrowserLink();// 浏览连接
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");//非开发模式显示标准错误信息页面
            //}

            app.UseDeveloperExceptionPage();//开发模式显示错误页面
            app.UseExceptionHandler("/Home/Error");//非开发模式显示标准错误信息页面

            #region --自定义中间件 代码写在外部--
            app.UseMyMiddleware();//使用自定义的中间件
            app.UseMyFactoryMiddleware("超人来了");// 使用自定义的工厂中间件
            #endregion

            #region --自定义中间件 代码在代码块中--
            /*
             *  (注意：Use【运行后可控制next】 和 Run【运行后不再运行】)
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
                Console.WriteLine("\nA\n");
                return async (context) =>
                {
                    // 1. 对Request做一些处理
                    // TODO

                    // 2. 调用下一个中间件
                    Console.WriteLine("\nA-BeginNext\n");
                    await next(context);
                    Console.WriteLine("\nA-EndNext\n");

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
                await context.Response.WriteAsync("This is my world! Enjoy! [this is the end!]");
                    // 此代码执行后，未向下继续执行。
                });
            #endregion
            #endregion

            #region --静态文件中间件--
            app.UseStaticFiles();// 静态文件
            //特殊解析的静态路径,将GodFS路径解析到目录GodFiles
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "GodFiles")),
                RequestPath = "/GodFS",
                // 还可写响应头
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age=4800");
                    ctx.Context.Response.Headers.Append("where", "god area");
                }
            });
            #endregion

            app.UseAuthentication();// 用户验证

            #region --路由配置--
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area", 
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            // 下面这个是老的core2.2的
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "areas",
            //        template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            //    // {id:int}类型约束
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //    /*
            //     * //下面这个模板配置和 template: "{controller=Home}/{action=Index}/{id?}"); 相同
            //     * routes.MapRoute(
            //     * name: "default_route",
            //     * template: "{controller}/{action}/{id?}",
            //     * defaults: new { controller = "Home", action = "Index" });
            //     */
            //    /*
            //     * // *号，全方位匹配路径，默认是：ReadArticle
            //     * // 此模板将匹配类似 /Blog/All-About-Routing/Introduction 的 URL 路径并提取值 { controller = Blog, action = ReadArticle, article = All-About-Routing/Introduction }。 controller 和 action 的默认路由值由路由生成，即便模板中没有对应的路由参数。 可在路由模板中指定默认值。 根据路由参数名称前的星号 外观，article 路由参数被定义为全方位*。 全方位路由参数可捕获 URL 路径的其余部分，也能匹配空白字符串。
            //     * // 不全懂！
            //     * routes.MapRoute(
            //     * name: "blog",
            //     * template: "Blog/{*article}",
            //     * defaults: new { controller = "Blog", action = "ReadArticle" });
            //     */
            //    /*
            //     * // 此模板与 /en-US/Products/5 等 URL 路径相匹配，并且提取值 { controller = Products, action = Details, id = 5 } 和数据令牌 { locale = en-US }。
            //     * routes.MapRoute(
            //     * name: "us_english_products",
            //     * template: "en-US/Products/{id}",
            //     * defaults: new { controller = "Products", action = "Details" },
            //     * constraints: new { id = new IntRouteConstraint() },
            //     * dataTokens: new { locale = "en-US" });
            //     */
            //});
            #endregion
        }
    }
}
