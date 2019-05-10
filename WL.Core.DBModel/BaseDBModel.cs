using System;

namespace WL.Core.DBModel
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class BaseDBModel
    {
        /// <summary>
        /// 核心编号
        /// </summary>
        public string CoreID { get; set; } = string.Empty;
        /// <summary>
        /// 是否可用
        /// </summary>
        public int Disabled { get; set; } = 0;
        /// <summary>
        /// 是否回收
        /// </summary>
        public int IsRecycle { get; set; } = 0;
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; } = 0;
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 最后编辑人
        /// </summary>
        public string Editor { get; set; } = string.Empty;
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime EditTime { get; set; } = DateTime.Now;
    }
}
