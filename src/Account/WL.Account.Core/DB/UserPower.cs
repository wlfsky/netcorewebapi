
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
    /// �û��ͽ�ɫ��ϵ��
    /// </summary>
    [Table("WL_UserPower")]
    public class UserPower : BaseDBModel
    {
        /// <summary>
        /// �û���ɫ��ϵid|
        /// </summary>
        public long URRID { get; set; } = 0;
        /// <summary>
        /// �û����|
        /// </summary>
        public long UserID { get; set; } = 0;
        /// <summary>
        /// ��ɫ���|
        /// </summary>
        public long RoleID { get; set; } = 0;
        /// <summary>
        /// ��ɫ·��|
        /// </summary>
        public string RolePath { get; set; } = string.Empty;
        /// <summary>
        /// �û���ɫӳ��ͼ|��ʽΪ���û�id|/��ɫ·��/��|���ڿ�������
        /// </summary>
        public string UserRoleMap { get; set; } = string.Empty;

    }
}
