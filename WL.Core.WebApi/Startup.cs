using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using WL.Core.BusinessService;

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
            //services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            services.Configure<FormOptions>(config => {
                config.ValueLengthLimit = int.MaxValue;
                config.MultipartBodyLengthLimit = int.MaxValue;
            });


            services.AddMvc();
            // 默认可以跨域
            services.AddCors((option) => option.AddPolicy("AllowCors", builder => builder.AllowAnyOrigin().AllowAnyMethod()));
            // 默认用大写开头和实体一致
            services.AddMvc().AddJsonOptions(options=>{ options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });

            #region --注入业务层  业务服务--
            services.AddSingleton<IUserSystemBLL, UserSystemBLL>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 配置 HTTP 请求管道
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //下面两句会触发两个日志，根据配置会显示到控制台窗口
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

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
                    template: "{controller=Home}/{action=Index}/{id?}");
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
