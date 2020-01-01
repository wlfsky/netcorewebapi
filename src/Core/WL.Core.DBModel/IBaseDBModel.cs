using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.DBModel
{
    public interface IBaseDBModel
    {
        /// <summary>
        /// 核心编号
        /// </summary>
        string CoreID { get; set; }
        /// <summary>
        /// 是否可用，0可用，1不可用
        /// </summary>
        int Disabled { get; set; }
        /// <summary>
        /// 是否回收，0不回收，1回收
        /// </summary>
        int IsRecycle { get; set; }
        /// <summary>
        /// 是否删除，0不删除，1删除
        /// </summary>
        int IsDel { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        string CreatorID { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后编辑人ID
        /// </summary>
        string EditorID { get; set; }
        /// <summary>
        /// 最后编辑人
        /// </summary>
        string Editor { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        DateTime EditTime { get; set; }
    }
}
