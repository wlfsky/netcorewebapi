using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 图片模型
    /// </summary>
    public class PicModel
    {
        /// <summary>
        /// 图片id
        /// </summary>
        public string ImgId { get; set; }
        /// <summary>
        /// 图片键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图片类型，扩展名
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 文件全名，文件名+扩展
        /// </summary>
        public string ImgFullName { get; set; }
        /// <summary>
        /// 图片字节尺寸
        /// </summary>
        public long ByteSize { get; set; }
        /// <summary>
        /// 图片大小尺寸
        /// </summary>
        public ImageSize PicSize { get; set; }
        /// <summary>
        /// 图片保存路径
        /// </summary>
        public string SavePath { get; set; }
    }
}
