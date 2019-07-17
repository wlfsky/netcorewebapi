﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using WL.Account.BusinessService;
using WL.Account.Model.Business.Interface;
using WL.Core.BusinessService;
using WL.Core.WebApi.Filter;

namespace WL.Core.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // 向容器内添加服务
        public void ConfigureServices(IServiceCollection services)
        {
            #region --配置日志--
            services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                }); 
            #endregion

            //services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");


            services.Configure<FormOptions>(config => {
                config.ValueLengthLimit = int.MaxValue;
                config.MultipartBodyLengthLimit = int.MaxValue;
            });

            // 添加过滤器
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(SurfaceAuthorizationFilter));
                options.Filters.Add(new GeneralActionFilter());// An instance 实例方式
                options.Filters.Add(typeof(GeneralResultFilter));// By type 类型方式
                
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // 默认可以跨域
            services.AddCors((option) => option.AddPolicy("AllowCors", builder => builder.AllowAnyOrigin().AllowAnyMethod()));
            // 默认用大写开头和实体一致
            services.AddMvc().AddJsonOptions(options=>{
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            #region --注入业务层  业务服务--
            services.AddSingleton<IAccountBLL, AccountBLL>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 配置 HTTP 请求管道
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region --老方式2.1的，2.2移动到了ConfigureServices方法中--
            ////下面两句会触发两个日志，根据配置会显示到控制台窗口
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug(); 
            #endregion

            app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/api/Error");
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Home}/{action=Index}/{id?}");
            });
            app.UseCors("AllowCors");
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("hello api");
            //});

            //

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseMvc();

        }
    }
}