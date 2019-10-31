
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Core.DBModel;

namespace WL.Account.Core.DB
{

    /// <summary>
    /// ϵͳ���ܱ�
    /// </summary>
    [Table("WL_Function")]
    public class SysFunction : BaseDBModel
    {
        /// <summary>
        /// ���ܱ��|
        /// </summary>
        public string FuncID { get; set; } = string.Empty;
        /// <summary>
        /// ϵͳ���ܱ�������̱���
        /// </summary>
        public string FuncAlias { get; set; } = string.Empty;
        /// <summary>
        /// ��������|
        /// </summary>
        public string FuncName { get; set; } = string.Empty;
        /// <summary>
        /// ���ܸ������|
        /// </summary>
        public string FuncPID { get; set; } = string.Empty;
        /// <summary>
        /// ���ܱ��·��|/id/id
        /// </summary>
        public string FuncIDPath { get; set; } = string.Empty;
        /// <summary>
        /// ���ܱ���·��|/name/name
        /// </summary>
        public string FuncPath { get; set; } = string.Empty;
        /// <summary>
        /// ���ܱ�ע|
        /// </summary>
        public string Remark { get; set; } = string.Empty;

    }
}
