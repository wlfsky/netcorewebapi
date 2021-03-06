﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WL.Account.BusinessService;
using WL.Account.Core.Business;
using WL.Account.Core.Business.Interface;
using WL.Core.BusinessService;
using WL.Core.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WL.Core.WebApp.Controllers
{
    public class IViewController : Controller
    {
        private readonly IAccountBLL _userSystemBLL;

        public IViewController(IAccountBLL userSystemBLL)
        {
            _userSystemBLL = userSystemBLL;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult User()
        {
            //var r = _userSystemBLL.GetList(new UserAccount());
            //return Json(r);
            return View();
        }
    }
}
