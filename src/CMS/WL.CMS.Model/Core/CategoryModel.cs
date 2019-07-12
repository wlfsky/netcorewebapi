using System;
using System.Collections.Generic;
using System.Text;
using WL.CMS.Model.DB;

namespace WL.CMS.Model
{
    /// <summary>
    /// 分类
    /// </summary>
    public class CategoryModel : CategoryDBModel
    {
        public CategoryModel RootCategory { get; set; }
        public CategoryModel ParentCategory { get; set; }

        public CategoryModel Parent { get; set; }

        public List<CategoryModel> ChildrenNodes { get; set; }
    }
}
