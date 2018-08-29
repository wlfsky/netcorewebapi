
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// ��ɫ���Ź��ܹ�ϵ��
    /// </summary>
    [Table("WL_RolePower")]
    public class RolePower : BaseModel
    {
        /// <summary>
        /// Ȩ�ޱ��|
        /// </summary>
        public long PowerID { get; set; } = 0;
        ///// <summary>
        ///// ���̱��|
        ///// </summary>
        //public int ProjectID { get; set; } = 0;
        /// <summary>
        /// ���ű��|
        /// </summary>
        public long DepID { get; set; } = 0;
        /// <summary>
        /// ����·��|/dep/dep
        /// </summary>
        public string DepIDPath { get; set; } = string.Empty;
        /// <summary>
        /// ��ɫ���|
        /// </summary>
        public long RoleID { get; set; } = 0;
        /// <summary>
        /// ��ɫ·��|/role/role
        /// </summary>
        public string RolePath { get; set; } = string.Empty;
        /// <summary>
        /// ���ܱ��|
        /// </summary>
        public long FuncID { get; set; } = 0;
        /// <summary>
        /// ����·��|/func/func
        /// </summary>
        public string FuncPath { get; set; } = string.Empty;
        /// <summary>
        /// Ȩ������|depid|role|funcid
        /// </summary>
        public string Coordinate { get; set; } = string.Empty;

    }
}
