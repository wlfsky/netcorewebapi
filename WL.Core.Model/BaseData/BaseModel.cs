using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Model
{
    [Obsolete("废弃: 新 WL.Core.DBModel.BaseDBModel", true)]
    [Serializable]
    public class BaseModel
    {
        /// <summary>
        /// 核心编号
        /// </summary>
        public long CoreID { get; set; } = 0;
        /// <summary>
        /// 创建时间|
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 创建人|
        /// </summary>
        public string CreateUser { get; set; } = string.Empty;
        /// <summary>
        /// 最后修改时间|
        /// </summary>
        public DateTime ModifyTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 最后修改人|
        /// </summary>
        public string ModifUser { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除|
        /// </summary>
        public int IsDel { get; set; } = 0;
    }
}
