using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.DataShell
{
    /// <summary>
    /// 操作人信息
    /// </summary>
    public interface IOperator
    {
        /// <summary>
        /// 操作人id
        /// </summary>
        string OperatorID { get; set; }

        /// <summary>
        /// 操作人名
        /// </summary>
        string OperatorName { get; set; }

    }

    /// <summary>
    /// 基本操作人
    /// </summary>
    public class BaseOperaror : IOperator
    {
        /// <summary>
        /// 操作人id
        /// </summary>
        public string OperatorID { get; set; }

        /// <summary>
        /// 操作人名
        /// </summary>
        public string OperatorName { get; set; }
    }
}
