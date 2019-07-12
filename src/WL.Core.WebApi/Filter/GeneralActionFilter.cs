using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WL.Core.WebApi.Filter
{
    /// <summary>
    /// 一般行为过滤器
    /// </summary>
    public class GeneralActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var x = 1;
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var x = 1;
            //throw new NotImplementedException();
        }
    }
}
