using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model.DB
{
    public class UserLevelDBModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public int UserLevel { get; set; }
        /// <summary>
        /// 用户等级名
        /// </summary>
        public string UserLevelName { get; set; }
        /// <summary>
        /// 总分数
        /// </summary>
        public long TotalScore { get; set; }
        /// <summary>
        /// 用户在此模块中的备注(后台可见，用户不可见)
        /// </summary>
        public string UserRemark { get; set; }
    }
}
