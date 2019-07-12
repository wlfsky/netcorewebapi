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
    public class MsgController : BaseApiController
    {
        [HttpGet]
        [HttpOptions]
        [Route("/api/[controller]/{contid}")]
        public DataShell<MsgModel> Get(string contid)
        {
            var res = new MsgModel() { MsgId = "1", MsgText = "msg text", MsgTime = DateTime.Now, IsRead = false };
            return res.Success();
        }
    }
}