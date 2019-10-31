
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
    /// ��ɫ���Ź��ܹ�ϵ��
    /// </summary>
    [Table("WL_RolePower")]
    public class RolePower : BaseDBModel
    {
        /// <summary>
        /// Ȩ�ޱ��|
        /// </summary>
        public long PowerID { get; set; } = 0;
        /// <summary>
        /// ���ű��|
        /// </summary>
        public long DepID { get; set; } = 0;
        /// <summary>
        /// ����·��|/dep/dep
        /// </summary>
        public string DepPath { get; set; } = string.Empty;
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
        /// Ȩ�����꣺dep|role|func
        /// </summary>
        public string Coordinate { get; set; } = string.Empty;

    }
}
