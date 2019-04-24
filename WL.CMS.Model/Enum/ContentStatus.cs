using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    public enum ContentStatus
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None = 0,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 1,
        /// <summary>
        /// 审核中
        /// </summary>
        Auditing = 2,
        /// <summary>
        /// 拒绝
        /// </summary>
        Rejected = 3,
        /// <summary>
        /// 批准
        /// </summary>
        Approved = 4,
        /// <summary>
        /// 撤销，批准后撤销
        /// </summary>
        Revocation = 5,
        /// <summary>
        /// 作废
        /// </summary>
        Obsolete = 6
    }
}
