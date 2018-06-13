
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{

    /// <summary>
    /// �û��ͽ�ɫ��ϵ��
    /// </summary>
    [Table("WL_UserPower")]
    public class UserPower : BaseModel
    {
        /// <summary>
        /// �û���ɫ��ϵid|
        /// </summary>
        public int URRID { get; set; } = 0;
        /// <summary>
        /// �û����|
        /// </summary>
        public int UserID { get; set; } = 0;
        /// <summary>
        /// ��ɫ���|
        /// </summary>
        public int RoleID { get; set; } = 0;
        /// <summary>
        /// ��ɫ·��|
        /// </summary>
        public string RoleIDPath { get; set; } = string.Empty;
        /// <summary>
        /// �û���ɫӳ��ͼ|���ڿ�������
        /// </summary>
        public string UserRoleMap { get; set; } = string.Empty;

    }
}
