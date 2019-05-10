using System;
using System.Collections.Generic;
using System.Text;
using WL.CMS.Model.DB;

namespace WL.CMS.Model
{
    /// <summary>
    /// 图片模型
    /// </summary>
    public class PicModel : FileDBModel
    {
        /// <summary>
        /// 图片大小尺寸
        /// </summary>
        public ImageSize PicSize { get; set; }
    }
}
