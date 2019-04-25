using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 内容列表类
    /// </summary>
    public class ContentListItemModel : BaseModel
    {
        /// <summary>
        /// 内容id
        /// </summary>
        public string ContentId { get; set; }
        /// <summary>
        /// 父级分类树
        /// </summary>
        public List<CategoryModel> ParentCategory { get; set; }
        /// <summary>
        /// 内容别名
        /// </summary>
        public string ContentAlias { get; set; }
        /// <summary>
        /// 别名路径
        /// </summary>
        public string AliasPath { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<TagModel> Tags { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public List<KeyModel> Keys { get; set; }
        /// <summary>
        /// 内容标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 创作时间
        /// </summary>
        public DateTime CreativeTime { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
        /// <summary>
        /// 主图地址
        /// </summary>
        public string MainPic { get; set; }
    }
}
