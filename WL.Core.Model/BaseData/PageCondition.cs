// ------------------------------------
// ProjectName : WL.X.Model
// FileName    : PageListCondition
// CreateTime  : 2017/8/14 18:08:14
// Creator     : weilai
// Remark      : 分页条件对象
// ------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WL.Core.Model
{
    /// <summary>
    /// PageListCondition
    /// </summary>
    public class PageCondition<T> : WlToolsLib.Pagination.PageCondition
    {
        public T ConditionObj { get; set; }
    }
}
