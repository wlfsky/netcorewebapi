
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// �û���
    /// </summary>
    [Table("WL_Employee")]
    public class Employee : BaseModel
    {
        /// <summary>
        /// ְԱ���|
        /// </summary>
        public long EmployeeID { get; set; } = 0;
        /// <summary>
        /// ְԱ����|
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;
        /// <summary>
        /// �Ա�|1��2Ů
        /// </summary>
        public int Sex { get; set; } = 0;
        /// <summary>
        /// �ֻ���|
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// ���֤������|
        /// </summary>
        public int IDType { get; set; } = 0;
        /// <summary>
        /// ���֤������|
        /// </summary>
        public string IDNum { get; set; } = string.Empty;
        /// <summary>
        /// ����|
        /// </summary>
        public DateTime Birthday { get; set; } = new DateTime();
        /// <summary>
        /// ����|
        /// </summary>
        public int Age { get; set; } = 0;
        /// <summary>
        /// ְ����|
        /// </summary>
        public string DutiesID { get; set; } = string.Empty;
        /// <summary>
        /// ְ��|
        /// </summary>
        public string DutiesName { get; set; } = string.Empty;
        /// <summary>
        /// ����ID|
        /// </summary>
        public int DepID { get; set; } = 0;
        /// <summary>
        /// ��������|
        /// </summary>
        public string DepName { get; set; } = string.Empty;
        /// <summary>
        /// ����·��|
        /// </summary>
        public string DepPath { get; set; } = string.Empty;
        /// <summary>
        /// ����|
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// �Ƿ���ְ|1��ְ2�ݼ�3��ְ4����
        /// </summary>
        public int JobStatus { get; set; } = 0;

    }
}
