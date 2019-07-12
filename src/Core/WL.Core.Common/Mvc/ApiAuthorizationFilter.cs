using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WL.Core.Common.Token;
using Microsoft.AspNetCore.Mvc;

namespace WL.Core.Common.Mvc
{
    public class ApiAuthorizationFilter : IAuthorizationFilter
    {
        private IToken tokenx = new DefaultInsideNetApiToken();
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            //
            StringValues tokens = new StringValues();
            if (context.HttpContext.Request.Headers.TryGetValue("session-token", out tokens))
            {
                var token = tokens.FirstOrDefault();
                if (tokenx.AuthenticationToken(token))
                {
                    RedirectToActionResult content = new RedirectToActionResult("NoAuth", "Exception", null);
                    context.Result = content;
                }
            }

        }
    }
}
