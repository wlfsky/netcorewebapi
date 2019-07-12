using System;
using System.Collections.Generic;
using System.Text;
using WL.Core.DBModel;

namespace WL.CMS.Model.DB
{
    public class FileDBModel : BaseDBModel
    {
        /// <summary>
        /// 文件编号
        /// </summary>
        public string FileId { get; set; }
        /// <summary>
        /// 文件键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 文件Hash值
        /// </summary>
        public string FileHashCode { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 文件全名，文件名+扩展
        /// </summary>
        public string FileFullName { get; set; }
        /// <summary>
        /// 文件尺寸
        /// </summary>
        public long ByteSize { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public long Duration { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 存储路径
        /// </summary>
        public string SavePath { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public DateTime SaveTime { get; set; }
        /// <summary>
        /// 根分类id
        /// </summary>
        public string RootCategoryID { get; set; }
        /// <summary>
        /// 根分类别名
        /// </summary>
        public string RootCategoryAlias { get; set; }
        /// <summary>
        /// 父级分类id
        /// </summary>
        public string ParentCategoryID { get; set; }
        /// <summary>
        /// 父级分类别名
        /// </summary>
        public string ParentCategoryAlias { get; set; }
        /// <summary>
        /// 内容id
        /// </summary>
        public string CantentID { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keys { get; set; }
    }
}
