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
    public class ContentController : BaseApiController
    {
        [HttpGet]
        [HttpOptions]
        [Route("/api/content/{contid}")]
        public IDataShell<ContentModel> Get(string contid)
        {

            return new ContentModel() { Title = "正文内容" }.Success();
        }
    }
}