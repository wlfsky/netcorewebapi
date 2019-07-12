using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 图片尺寸信息
    /// </summary>
    public class ImageSize
    {
        /// <summary>
        /// 图片宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 图片高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 图片时长，小数点前秒，只适合gif
        /// </summary>
        public decimal Duration { get; set; }
        /// <summary>
        /// 帧数，只适合gif
        /// </summary>
        public int FrameNum { get; set; }

    }
}
