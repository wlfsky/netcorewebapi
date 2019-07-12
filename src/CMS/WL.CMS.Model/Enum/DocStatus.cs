using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 文档状态
    /// </summary>
    public enum DocStatus
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None = 0,
        /// <summary>
        /// 数据库
        /// </summary>
        Database = 1,
        /// <summary>
        /// 数据库和静态文件
        /// </summary>
        DBAndStatic = 2,
        /// <summary>
        /// 静态文件，无数据
        /// </summary>
        StaticFile = 3,
        /// <summary>
        /// 归档文件，无数据，无静态
        /// </summary>
        DocFile = 4,
        /// <summary>
        /// 失落状态，无数据，无静态，无归档
        /// </summary>
        Loss = 5
    }
}
