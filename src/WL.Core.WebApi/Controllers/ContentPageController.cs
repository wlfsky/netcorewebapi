using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WL.Core.Model.CMSModel.AppModel;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentPageController : BaseApiController
    {
        [HttpPost]
        [HttpOptions]
        public IDataShell<PageShell<ContentListItemModel>> Get(PageCondition<ContentsPageCondition> param)
        {
            var res = new PageShell<ContentListItemModel>() { Rows = new List<ContentListItemModel>(), PageIndex = 1, PageSize = 10, RecordCount = 12 };
            res.Rows.Add(new ContentListItemModel() { Title = "1", Summary = "11", ContentId = "1" });
            res.Rows.Add(new ContentListItemModel() { Title = "2", Summary = "22", ContentId = "2" });
            res.Rows.Add(new ContentListItemModel() { Title = "3", Summary = "33", ContentId = "3" });
            return res.Success();
        }
    }
}
