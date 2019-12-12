using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WL.Core.Model.CMSModel.AppModel;
using WlToolsLib.DataShell;

namespace WL.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : BaseApiController
    {
        [HttpGet]
        [HttpOptions]
        public IDataShell<AboutInfoModel> Get()
        {
            var res = new AboutInfoModel();
            res.OpenSourceInfo = new List<OpenSourceInfoModel>();
            res.WebSite = "";
            res.OpenSourceInfo.Add(new OpenSourceInfoModel() { OpenSourceName = "XXX", Version = "1.1" });
            return res.Success();

        }
    }
}