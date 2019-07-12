using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Model.CMSModel.AppModel
{
    public class CategoryModel
    {
        public string CateId { get; set; }
        public string ParentId { get; set; }
        public string CidPath { get; set; }
        public string Alias { get; set; }
        public string AliasPath { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public int NewCount { get; set; }
        public int TotalCount { get; set; }
    }
}
