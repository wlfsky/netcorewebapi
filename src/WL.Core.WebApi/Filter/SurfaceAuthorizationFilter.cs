using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WL.Core.WebApi.Filter
{
    /// <summary>
    /// 表面身份验证过滤器
    /// </summary>
    public class SurfaceAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var x = 1;
            //throw new NotImplementedException();
        }
    }
}
