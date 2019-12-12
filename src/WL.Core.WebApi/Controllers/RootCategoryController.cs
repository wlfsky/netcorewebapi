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
    public class RootCategoryController : BaseApiController
    {
        [HttpGet]
        [HttpOptions]
        public IDataShell<List<CategoryModel>> Get()
        {
            List<CategoryModel> cate = new List<CategoryModel>();
            cate.Add(new CategoryModel() { CateId ="1", Alias="cs", NewCount = 1, TotalCount = 10, Title= "C#", Remark = "c#备注", ParentId = "" });
            cate.Add(new CategoryModel() { CateId ="2", Alias="py", NewCount = 1, TotalCount = 10, Title= "Python", Remark = "Python备注", ParentId = "" });
            cate.Add(new CategoryModel() { CateId ="3", Alias="rust", NewCount = 1, TotalCount = 10, Title= "Rust", Remark = "Rust备注", ParentId = "" });
            cate.Add(new CategoryModel() { CateId ="4", Alias="go", NewCount = 1, TotalCount = 10, Title= "Go", Remark = "Go备注", ParentId = "" });
            return cate.Success();
        }
    }
}