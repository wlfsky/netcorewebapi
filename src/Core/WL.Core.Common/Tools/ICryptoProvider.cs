using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Common
{
    /// <summary>
    /// 数据加密解密传输接口
    /// </summary>
    public interface ICryptoProvider
    {
        #region --传输加解密--
        /// <summary>
        /// 传输加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        string TransferEncryption(string sourceStr);

        /// <summary>
        /// 传输解密
        /// </summary>
        /// <param name="cipherStr"></param>
        /// <returns></returns>
        string TransferDecryption(string cipherStr);
        #endregion

        /// <summary>
        /// 密码Hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string UserPasswordHash(string password);

        #region --Token加解密和hash--
        /// <summary>
        /// Token的Hash
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        string TokenHash(string tokenStr);

        /// <summary>
        /// token 加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        string TokenEncryption(string sourceStr);

        /// <summary>
        /// token 解密
        /// </summary>
        /// <param name="cipherStr"></param>
        /// <returns></returns>
        string TokenDecryption(string cipherStr);
        #endregion
    }
}
