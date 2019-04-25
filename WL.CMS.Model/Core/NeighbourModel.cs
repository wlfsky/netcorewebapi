using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 相邻内容模型
    /// </summary>
    public class NeighbourModel
    {
        /// <summary>
        /// 相邻内容ID
        /// </summary>
        public string ContentId { get; set; }
        /// <summary>
        /// 相邻内容标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 相邻内容显示标题
        /// </summary>
        public string ShowTitle { get; set; }
        /// <summary>
        /// 相邻内容url
        /// </summary>
        public string NeighbourUrl { get; set; }
    }
}
