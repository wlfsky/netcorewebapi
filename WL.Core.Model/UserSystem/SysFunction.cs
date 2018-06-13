
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// ϵͳ���ܱ�
    /// </summary>
    [Table("WL_Function")]
    public class SysFunction : BaseModel
    {
        /// <summary>
        /// ���ܱ��|
        /// </summary>
        public int FuncID { get; set; } = 0;
        /// <summary>
        /// ��������|
        /// </summary>
        public string FuncName { get; set; } = string.Empty;
        /// <summary>
        /// ���ܸ������|
        /// </summary>
        public int FuncPID { get; set; } = 0;
        /// <summary>
        /// ���ܱ��·��|/id/id
        /// </summary>
        public string FuncIDPath { get; set; } = string.Empty;
        /// <summary>
        /// ��������·��|/name/name
        /// </summary>
        public string FuncNamePath { get; set; } = string.Empty;
        /// <summary>
        /// ���ܱ�ע|
        /// </summary>
        public string Remark { get; set; } = string.Empty;

    }
}
