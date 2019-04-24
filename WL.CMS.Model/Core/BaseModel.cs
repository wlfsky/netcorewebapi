using System;

namespace WL.CMS.Model
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public int Disabled { get; set; }
        /// <summary>
        /// 是否回收
        /// </summary>
        public int IsRecycle { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set;}
        /// <summary>
        /// 最后编辑人
        /// </summary>
        public string Editor { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime EditTime { get; set; }
    }
}
