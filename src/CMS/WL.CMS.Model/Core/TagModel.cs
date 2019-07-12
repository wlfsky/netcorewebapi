using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 标签实体
    /// </summary>
    public class TagModel : BaseModel
    {
        /// <summary>
        /// 标签id
        /// </summary>
        public string TagID { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
    }
}
