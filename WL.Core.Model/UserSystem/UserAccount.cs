
using Dapper;
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
    [Table("WL_ACCOUNT")]
    public class UserAccount : BaseModel
    {
        /// <summary>
        /// �û����|
        /// </summary>
        [Key]
        //[Required]
        public long UserID { get; set; } = 0;
        /// <summary>
        /// �û���¼��|
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// �û�����|
        /// </summary>
        public string UserPassword { get; set; } = string.Empty;
        /// <summary>
        /// �û��ֻ���|
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// �û��ǳ�|
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// �û�����|
        /// </summary>
        public string RealName { get; set; } = string.Empty;
        /// <summary>
        /// �û������ʼ�|
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// ��������|
        /// </summary>
        public string HighPsaaword { get; set; } = string.Empty;
        /// <summary>
        /// ��ʱ���������һ�����|
        /// </summary>
        public string TempPassword { get; set; } = string.Empty;
        /// <summary>
        /// ��ʱ����ʧЧʱ��|
        /// </summary>
        public DateTime TempPassOverTime { get; set; } = new DateTime();
        /// <summary>
        /// ע��ʱ��|
        /// </summary>
        public DateTime RegistTime { get; set; } = new DateTime();
        /// <summary>
        /// ��¼����|
        /// </summary>
        public float LoginTimes { get; set; } = 0f;
        /// <summary>
        /// ����¼ʱ��|
        /// </summary>
        public DateTime LastLoginTime { get; set; } = new DateTime();
        /// <summary>
        /// ����ְԱ���|
        /// </summary>
        public string EmployeeID { get; set; } = string.Empty;
        /// <summary>
        /// ������Ŀ���|
        /// </summary>
        // public string ProjectID { get; set; } = string.Empty;
        /// <summary>
        /// �ʺ��Ƿ����|0δ����1����
        /// </summary>
        public int IsForbid { get; set; } = 0;

    }
}
