using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 内容
    /// </summary>
    public class ContentModel
    {
        /// <summary>
        /// 内容id
        /// </summary>
        public string ContentId { get; set; }
        /// <summary>
        /// 内容根分类
        /// </summary>
        public CategoryModel RootCategory { get; set; }
        /// <summary>
        /// 内容父级分类树列表
        /// </summary>
        public List<CategoryModel> ParentCategory { get; set; }
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
        /// 内容正文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 内容备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 创作时间
        /// </summary>
        public DateTime CreativeTime { get; set; }
        public DateTime LastEditTime { get; set; }
        public string MainPic { get; set; }

        #region --控制段--
        /// <summary>
        /// 内容状态
        /// </summary>
        public ContentStatus Status { get; set; }
        /// <summary>
        /// 审判日志
        /// </summary>
        public List<JudgeLog> JudgeLogs { get; set; }

        /// <summary>
        /// 操作日志
        /// </summary>
        public List<OperationLog> OperaLogs { get; set; }
        #endregion

        public List<FileModel> Files { get; set; }
        public List<PicModel> Pics { get; set; }
        public List<VideoModel> Videos { get; set; }
        public List<LogModel> Logs { get; set; }
        public NeighbourModel Previous { get; set; }
        public NeighbourModel Next { get; set; }
        public SysInfoModel SysInfo { get; set; }
    }
}
