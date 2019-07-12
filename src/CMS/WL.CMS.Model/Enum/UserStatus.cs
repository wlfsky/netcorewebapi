using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model.Enum
{
    public enum UserStatus
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None= 0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 20,
        /// <summary>
        /// 锁定
        /// </summary>
        Locked = 30,
        /// <summary>
        /// 被限制
        /// </summary>
        Confined = 50,
        /// <summary>
        /// 不朽
        /// </summary>
        Eternity = 99
    }
}
