using System;
using WlToolsLib.DataShell;

namespace WL.Core.DataCommon
{
    /// <summary>
    /// 数据层扩展方法
    /// </summary>
    public static class ExtensionMethod
    {
        /// <summary>
        /// 根据异常信息生成一个失败的DataShell
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DataShell<T> ToFailShell<T>(this Exception self)
        {
            // 用错误信息创建失败，且加入堆栈信息
            return self.Message.Fail<T>().AddInfo(self.StackTrace);
        }
    }
}
