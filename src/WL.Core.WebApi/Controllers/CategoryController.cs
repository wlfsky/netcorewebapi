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
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        [HttpOptions]
        [Route("/api/category/{cateid}")]
        public DataShell<List<CategoryModel>> Get(string cateid)
        {
            List<CategoryModel> cate = new List<CategoryModel>();
            cate.Add(new CategoryModel() { CateId = "11", Alias = "csbase", NewCount = 1, TotalCount = 10, Title = "C#基础", Remark = "C#基础", ParentId = "" });
            cate.Add(new CategoryModel() { CateId = "12", Alias = "csskills", NewCount = 1, TotalCount = 10, Title = "C#技巧", Remark = "C#技巧", ParentId = "" });
            return cate.Success();

        }
    }
}