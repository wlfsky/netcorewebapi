using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    public class CategoryModel : BaseModel
    {
        public string CateId { get; set; }
        public string ParentId { get; set; }
        public string RootId { get; set; }
        public string Alias { get; set; }
        public string CidPath { get; set; }
        public string AliasPath { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public int SortNum { get; set; }
        public int NewCount { get; set; }
        public int TotalCount { get; set; }
    }
}
