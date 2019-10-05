using System;
using WlToolsLib.DataShell;

namespace WL.Core.BusinessCommon
{
    /// <summary>
    /// 用户会话
    /// </summary>
    public interface IUserSession
    {
        /// <summary>
        /// 保存会话
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessData"></param>
        /// <param name="sessKey">外部给定一个会话key</param>
        /// <returns></returns>
        public DataShell<string> SaveSession<T>(T sessData, string sessKey = "");
        /// <summary>
        /// 获取会话
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessKey"></param>
        /// <returns></returns>
        public DataShell<T> GetSession<T>(string sessKey);
        /// <summary>
        /// 刷新会话
        /// </summary>
        /// <returns></returns>
        public DataShell<string> RefreshSession();
    }
}
