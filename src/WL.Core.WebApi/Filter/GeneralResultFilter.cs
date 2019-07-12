using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WL.Core.WebApi.Filter
{
    public class GeneralResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            var x = 1;
            //throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var x = 1;
            //throw new NotImplementedException();
        }
    }
}
