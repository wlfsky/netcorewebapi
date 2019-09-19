using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Model.BaseData
{
    public class BaseQueryCondition
    {
        /// <summary>
        /// 是否可用，0可用，1不可用
        /// </summary>
        public int? Disabled { get; set; } = 0;
        /// <summary>
        /// 是否回收，0不回收，1回收
        /// </summary>
        public int? IsRecycle { get; set; } = 0;
        /// <summary>
        /// 是否删除，0不删除，1删除
        /// </summary>
        public int IsDel { get; set; } = 0;
    }
}
