using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.Expand
{
    /// <summary>
    /// 数字类型的扩展
    /// </summary>
    public static class NumberExpand
    {
        #region --decimal --
        /// <summary>
        /// 金额数据格式化
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToMoney(this decimal self)
        {
            return self.ToString("f2");
        } 
        #endregion
    }
}
