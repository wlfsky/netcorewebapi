using System;
using System.Collections.Generic;
using System.Text;
using WL.CMS.Model.DB;

namespace WL.CMS.Model
{
    /// <summary>
    /// 视频数据模型
    /// </summary>
    public class VideoModel : FileDBModel
    {
        /// <summary>
        /// 视频信息
        /// </summary>
        public string VideoInfo { get; set; }
    }
}
