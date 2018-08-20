// ------------------------------------
// ProjectName: $safeprojectname$
// FileName:    PageCondition
// CreateTime:  2017/07/13 17:55:46
// Creator:     weilai
// FileRemark:  
// ------------------------------------


namespace WlToolsLib.Pagination
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    /// 分页查询条件实体
    /// </summary>
    /// <typeparam name="TCondition"></typeparam>
    public class PageCondition<TCondition>
    {
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页数据尺寸
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 查新条件
        /// </summary>
        public TCondition Condition { get; set; }
    }
}
