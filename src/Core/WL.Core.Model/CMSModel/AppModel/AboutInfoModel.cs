using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Model.CMSModel.AppModel
{
    public class AboutInfoModel
    {
        public string WebSite { get; set; }
        public string WebSiteVersion { get; set; }
        public string Remark { get; set; }
        public string ImgSrc { get; set; }
        public string LegalInfo { get; set; }
        public List<OpenSourceInfoModel> OpenSourceInfo { get; set; }
    }
}
