using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Account.Core.Enum
{
    /// <summary>
    /// 帐号状态
    /// </summary>
    public enum AccountStatus
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None = 0,
        /// <summary>
        /// 注册验证
        /// </summary>
        RegistVerify = 10,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 20,
        /// <summary>
        /// 被锁定
        /// </summary>
        Locked = 30,
        /// <summary>
        /// 不安全
        /// </summary>
        Unsafe = 40,
        /// <summary>
        /// 冻结
        /// </summary>
        Freeze = 50,
        /// <summary>
        /// 再激活验证
        /// </summary>
        Reactivation = 60,
        /// <summary>
        /// 销毁
        /// </summary>
        Destroyed = 80,
        /// <summary>
        /// 永恒
        /// </summary>
        Eternal = 99
    }
}
