using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Model.CMSModel.AppModel
{
    public class ContentListItemModel
    {
        public string ContentId { get; set; }
        public List<CategoryModel> ParentCategory { get; set; }
        public string ContentAlias { get; set; }
        public string AliasPathte { get; set; }
        public List<string> Keys { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastEditTime { get; set; }
        public string MainPic { get; set; }
    }
}
