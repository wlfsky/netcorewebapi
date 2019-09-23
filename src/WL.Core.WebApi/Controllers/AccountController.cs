using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WL.Account.BusinessBridge;
using WL.Account.BusinessService;
using WL.Account.Model.Business;
using WL.Account.Model.Business.Interface;
using WL.Core.BusinessService;
using WL.Core.Model;
using WL.Core.WebApi.Common;
using WL.Core.WebApi.Filter;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Core.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountBLL _userSystemBLL; //#构造函数注入

        public AccountController(IAccountBLL userSystemBLL, ILogger<BaseApiController> log):base(log)
        {
            _userSystemBLL = new AccountBusinessBridge();
        }

        // POST api/user
        [HttpPost]
        [TypeFilter(typeof(WebApiErrorHandleAttribute))]
        public DataShell<AccountModel> Get(AccountModel req)
        {
            //string info = req;
            LogInfo("use /user/get 超级日志 info");
            //_logger.LogWarning("use /user/get 超级日志 warning");
            //_logger.LogError("use /user/get 超级日志 error");
            //var r = new DataShell<IEnumerable<UserAccount>>();// _userSystemBLL.GetList(new UserAccount());
            return _userSystemBLL.Get(req);
        }

        [HttpPost]
        [TypeFilter(typeof(WebApiErrorHandleAttribute))]
        public DataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            //string info = req;
            LogInfo("use /user/getpage");
            return _userSystemBLL.GetPage(condition);
        }

        [HttpPost]
        public async Task<DataShell<AccountModel>> Insert(AccountModel user)
        {
            //throw new Exception("异常演示");
            // 抛出错误后会被中间件引导到error控制器

            var res = _userSystemBLL.Insert(user);

            var r = await res.ToTask(() => res);//阻塞了～！为什么？
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