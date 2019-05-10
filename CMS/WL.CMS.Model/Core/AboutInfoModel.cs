using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 关于信息
    /// </summary>
    public class AboutInfoModel
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public string WebSite { get; set; }
        /// <summary>
        /// 网站版本
        /// </summary>
        public string WebSiteVersion { get; set; }
        /// <summary>
        /// 网站备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 网站图片
        /// </summary>
        public string ImgSrc { get; set; }
        /// <summary>
        /// 网站icon地址
        /// </summary>
        public string IconUrl { get; set; }
        /// <summary>
        /// 法律信息
        /// </summary>
        public string LegalInfo { get; set; }
        /// <summary>
        /// 开源信息
        /// </summary>
        public List<OpenSourceInfoModel> OpenSourceInfo { get; set; }
        /// <summary>
        /// 通用关键字信息
        /// </summary>
        public List<KeyModel> WebSiteKeys { get; set; }
        /// <summary>
        /// 网站内容
        /// </summary>
        public string WebSiteContent { get; set; }
        /// <summary>
        /// SEO信息
        /// </summary>
        public string WebSiteSEO { get; set; }
    }
}
