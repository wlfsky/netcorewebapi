using System;
using System.Collections.Generic;
using System.Text;
using WL.CMS.Model.DB;

namespace WL.CMS.Model
{
    /// <summary>
    /// 内容
    /// </summary>
    public class ContentModel : ContentDBModel
    {
        /// <summary>
        /// 内容根分类
        /// </summary>
        public CategoryModel RootCategory { get; set; }
        /// <summary>
        /// 内容父级分类树列表
        /// </summary>
        public List<CategoryModel> ParentCategory { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<TagModel> Tags { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public List<KeyModel> Keys { get; set; }

        #region --控制段--
        /// <summary>
        /// 审判日志
        /// </summary>
        public List<JudgeLog> JudgeLogs { get; set; }

        /// <summary>
        /// 操作日志
        /// </summary>
        public List<OperationLog> OperaLogs { get; set; }
        /// <summary>
        /// 日志附件
        /// </summary>
        public List<LogModel> Logs { get; set; }
        #endregion


        #region --附件信息--
        /// <summary>
        /// 文件附件
        /// </summary>
        public List<FileModel> Files { get; set; }
        /// <summary>
        /// 图片附件
        /// </summary>
        public List<PicModel> Pics { get; set; }
        /// <summary>
        /// 视频附件
        /// </summary>
        public List<VideoModel> Videos { get; set; }
        
        #endregion
        /// <summary>
        /// 前一个邻居内容
        /// </summary>
        public NeighbourModel Previous { get; set; }
        /// <summary>
        /// 后一个邻居内容
        /// </summary>
        public NeighbourModel Next { get; set; }
        /// <summary>
        /// 系统信息
        /// </summary>
        public SysInfoModel SysInfo { get; set; }
    }
}
