
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
    /// ��ɫ��
    /// </summary>
    [Table("WL_Role")]
    public class Role : BaseDBModel
    {
        /// <summary>
        /// ��ɫ���|
        /// </summary>
        public string RoleID { get; set; } = string.Empty;
        /// <summary>
        /// ��ɫ����|���ɽ�ɫ��·��
        /// </summary>
        public string RoleAlias { get; set; } = string.Empty;
        /// <summary>
        /// ��ɫ����|
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
        /// <summary>
        /// ��ɫ�������|
        /// </summary>
        public string RolePID { get; set; } = string.Empty;
        /// <summary>
        /// ��ɫ���·��|/id/id
        /// </summary>
        public string RoleIDPath { get; set; } = string.Empty;
        /// <summary>
        /// ��ɫ����·��|/name/name
        /// </summary>
        public string RolePath { get; set; } = string.Empty;
        /// <summary>
        /// ��ɫ��ע|
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// �Ƿ�̳�Ȩ��|0���̳�1�̳�
        /// </summary>
        public int IsInherit { get; set; } = 0;

    }
}
