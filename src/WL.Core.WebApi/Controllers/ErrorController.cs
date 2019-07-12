using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WlToolsLib.DataShell;

namespace WL.Core.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("/Error")]
    [Route("api/Error")]
    public class ErrorController : Controller
    {

        
        public DataShell<string> Index(string msg)
        {
            return $"发生了一个令程序员毛骨悚然的恐怖错误！({msg})".Fail<string>();
        }
    }
}