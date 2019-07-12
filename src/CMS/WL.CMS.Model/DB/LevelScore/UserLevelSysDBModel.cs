using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model.DB
{
    public class UserLevelSysDBModel
    {
        /// <summary>
        /// 等级分类
        /// </summary>
        public string LevelCategoryID { get; set; }
        /// <summary>
        /// 等级id
        /// </summary>
        public string LevelID { get; set; }
        /// <summary>
        /// 等级级别
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 等级名称
        /// </summary>
        public string LevelName { get; set; }
        /// <summary>
        /// 等级分数
        /// </summary>
        public long LevelScore { get; set; }
    }
}
