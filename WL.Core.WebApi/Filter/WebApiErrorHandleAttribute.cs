using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WL.Core.WebApi.Common;
using WL.Core.WebApi.Controllers;
using WlToolsLib.DataShell;
using WlToolsLib.Expand;

namespace WL.Core.WebApi.Filter
{
    public class WebApiErrorHandleAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public WebApiErrorHandleAttribute(
        IHostingEnvironment hostingEnvironment,
        IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }


        public override void OnException(ExceptionContext context)
        {
            
            var ex = context.Exception;
            var NewLine = Constant.RNNewline;
            // 生成错误信息
            Func<Exception, string> errorFormatter = (exc) => $"{NewLine}[MSG_B]{exc.Message}[MSG_E]{NewLine}[SOURCE_B]{exc.Source}[SOURCE_E]{NewLine}[TARGET_B]{exc.TargetSite}[TARGET_E]{NewLine}[STACK_B]{exc.StackTrace}[STACK_E]{NewLine}";
            //BaseApiController.Log4.ErrorLog(errorFormatter(ex));
            if (!_hostingEnvironment.IsDevelopment())
            {
                // do nothing
                return;
            }
            var result = new JsonResult("有错误发生！".Fail<string>());
            result.StatusCode = 500;
            //result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            //result.ViewData.Add("Exception", context.Exception);
            // TODO: Pass additional detailed data via ViewData
            context.Result = result;
            base.OnException(context);
        }

    }
}
