using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WlToolsLib.DataShell;

namespace WL.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : BaseApiController
    {
        [HttpGet]
        [Route("/")]
        [Route("/api")]
        public DataShell<string> Index()
        {
            return "欢迎使用 WL.Core.WebApi 2019".Success(info: "欢迎信息");
        }
    }
}