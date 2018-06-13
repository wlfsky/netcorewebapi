using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WL.Core.BusinessService;
using WL.Core.Model;
using WL.Core.WebApi.Filter;
using WlToolsLib.DataShell;

namespace WL.Core.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/UserSystem")]
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
            var r = _userSystemBLL.GetList(new UserAccount());
            return r;
        }

        [HttpGet]
        [Route("gets")]
        public DataShell<string> Get()
        {
            //throw new Exception("异常演示");
            // 抛出错误后会被中间件引导到error控制器
            
            
            return "fail".Success<string>();
        }
    }
}