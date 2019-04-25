using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 关键字
    /// </summary>
    public class KeyModel : BaseModel
    {
        /// <summary>
        /// 关键字id
        /// </summary>
        public string KeyID { get; set; }
        /// <summary>
        /// 关键字内容
        /// </summary>
        public string Key { get; set; }
    }
}
