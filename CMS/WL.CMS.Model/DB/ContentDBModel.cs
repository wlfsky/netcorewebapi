using System;
using System.Collections.Generic;
using System.Text;
using WL.Core.DBModel;

namespace WL.CMS.Model.DB
{
    /// <summary>
    /// 内容
    /// </summary>
    public class ContentDBModel : BaseDBModel
    {
        /// <summary>
        /// 内容id
        /// </summary>
        public string ContentId { get; set; }
        /// <summary>
        /// 内容根分类
        /// </summary>
        public string RootCategoryID { get; set; }
        /// <summary>
        /// 根分类别名
        /// </summary>
        public string RootCategoryAlias { get; set; }
        /// <summary>
        /// 内容父级分类树列表
        /// </summary>
        public string ParentCategoryID { get; set; }
        /// <summary>
        /// 父级分类别名
        /// </summary>
        public string ParentCategoryAlias { get; set; }
        /// <summary>
        /// 内容别名
        /// </summary>
        public string ContentAlias { get; set; }
        /// <summary>
        /// 内容id路径
        /// </summary>
        public string ContIDPath { get; set; }
        /// <summary>
        /// 别名路径
        /// </summary>
        public string AliasPath { get; set; }
        /// <summary>
        /// 内容标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 内容正文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 内容备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 显示的作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 主图地址
        /// </summary>
        public string MainPic { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string TagStr { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyStr { get; set; }
        #region --关键时间点--
        /// <summary>
        /// 创作时间
        /// </summary>
        public DateTime CreativeTime { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime LastEditTime { get; set; }
        /// <summary>
        /// 批准时间
        /// </summary>
        public DateTime ApprovedTime { get; set; }
        /// <summary>
        /// 撤销时间
        /// </summary>
        public DateTime RevocationTime { get; set; }
        /// <summary>
        /// 作废时间
        /// </summary>
        public DateTime ObsoleteTime { get; set; }
        #endregion
        #region --文章所属人--
        /// <summary>
        /// 内容所属人id
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 内容所属人名称
        /// </summary>
        public string UserName { get; set; }
        #endregion
        #region --控制段--
        /// <summary>
        /// 内容状态
        /// </summary>
        public ContentStatus Status { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }
        #endregion

        /// <summary>
        /// 前一个邻居内容
        /// </summary>
        public string PreviousCategoryID { get; set; }
        /// <summary>
        /// 后一个邻居内容
        /// </summary>
        public string NextCategoryID { get; set; }
        /// <summary>
        /// 系统信息
        /// </summary>
        public string SysRemark { get; set; }
    }
}
