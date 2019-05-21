using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WL.Account.BusinessService;
using WL.Account.Model.Business;
using WL.Core.BusinessService;
using WL.Core.Model;
using WL.Core.WebApi.Common;
using WL.Core.WebApi.Filter;
using WlToolsLib.DataShell;

namespace WL.Core.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/UserSystem")]
    //[ApiController]
    public class UserSystemController : BaseApiController
    {
        private readonly IUserSystemBLL _userSystemBLL; //#构造函数注入

        public UserSystemController(IUserSystemBLL userSystemBLL, ILogger<BaseApiController> log):base(log)
        {
            _userSystemBLL = userSystemBLL;
        }

        // POST api/user
        [HttpGet]
        [TypeFilter(typeof(WebApiErrorHandleAttribute))]
        public DataShell<IEnumerable<UserAccount>> Get([FromQuery]DataShell<string> value)
        {
            string info = value.Data;
            LogInfo("use /user/get 超级日志 info");
            //_logger.LogWarning("use /user/get 超级日志 warning");
            //_logger.LogError("use /user/get 超级日志 error");
            var r = new DataShell<IEnumerable<UserAccount>>();// _userSystemBLL.GetList(new UserAccount());
            return r;
        }

        [HttpGet]
        [Route("gets")]
        public async Task<ActionResult<DataShell<string>>> Get()
        {
            //throw new Exception("异常演示");
            // 抛出错误后会被中间件引导到error控制器

            var r = await "fail".Success<string>().ToTaskActResult();//阻塞了～！为什么？
            return r;
        }
    }
}