using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WL.Core.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Home/[action]")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Main")]
        public IActionResult Main()
        {
            return View();
        }
    }
}