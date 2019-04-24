using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 日志模型
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// 日志编号
        /// </summary>
        public string LogID { get; set; }
        /// <summary>
        /// 日志操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Log { get; set; }
        /// <summary>
        /// 日志备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }
        /// <summary>
        /// 所属编号
        /// </summary>
        public string BelongToID { get; set; }
        /// <summary>
        /// 日志类别
        /// </summary>
        public string LogCategory { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 类别表
        /// </summary>
        public string CategoryDBTable { get; set; }
    }
}
