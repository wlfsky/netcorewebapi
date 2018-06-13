
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// ��ɫ��
    /// </summary>
    [Table("WL_Role")]
    public class Role : BaseModel
    {
        /// <summary>
        /// ��ɫ���|
        /// </summary>
        public int RoleID { get; set; } = 0;
        /// <summary>
        /// ��ɫ����|
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
        /// <summary>
        /// ��ɫ�������|
        /// </summary>
        public int RolePID { get; set; } = 0;
        /// <summary>
        /// ��ɫ���·��|/id/id
        /// </summary>
        public string RoleIDPath { get; set; } = string.Empty;
        /// <summary>
        /// ��ɫ����·��|/name/name
        /// </summary>
        public string RoleNamePath { get; set; } = string.Empty;
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
