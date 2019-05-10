
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Core.DBModel;

namespace WL.Company.Model
{

    /// <summary>
    /// ���ű�
    /// </summary>
    [Table("WL_Dep")]
    public class Department : BaseDBModel
    {
        /// <summary>
        /// ���ű��|
        /// </summary>
        public long DepID { get; set; } = 0;
        /// <summary>
        /// ��������|
        /// </summary>
        public string DepName { get; set; } = string.Empty;
        /// <summary>
        /// ���Ÿ������|
        /// </summary>
        public int DepPID { get; set; } = 0;
        /// <summary>
        /// ���ű��·��|/id/id
        /// </summary>
        public string DepIDPath { get; set; } = string.Empty;
        /// <summary>
        /// ��������·��|/name/name
        /// </summary>
        public string DepNamePath { get; set; } = string.Empty;
        /// <summary>
        /// ���ű�ע|
        /// </summary>
        public string Remark { get; set; } = string.Empty;

    }
}