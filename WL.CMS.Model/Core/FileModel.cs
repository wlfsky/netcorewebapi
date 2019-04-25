using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 文件模型
    /// </summary>
    public class FileModel
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
        /// 存储路径
        /// </summary>
        public string Path { get; set; }
    }
}
