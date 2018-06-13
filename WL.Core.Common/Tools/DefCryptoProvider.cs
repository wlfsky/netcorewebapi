using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.CryptoHelper;

namespace WL.Core.Common
{
    /// <summary>
    /// 默认的加密解密数据传输提供者
    /// </summary>
    public class DefCryptoProvider : ICryptoProvider
    {
        public DefCryptoProvider()
        {
            this.KeyStr = "0123456789ABCDEF";
        }

        #region --传输加解密--
        /// <summary>
        /// 传输加解密Key字符 16位
        /// </summary>
        protected string KeyStr { get; set; }

        /// <summary>
        /// 对称加解密-解密
        /// </summary>
        /// <param name="cipherStr"></param>
        /// <returns></returns>
        public string TransferDecryption(string cipherStr)
        {
            return cipherStr.AESCBCDecryption(this.KeyStr);
        }

        /// <summary>
        /// 对称加解密-加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public string TransferEncryption(string sourceStr)
        {
            return sourceStr.AESCBCEncryption(this.KeyStr);
        }
        #endregion

        /// <summary>
        /// 用户密码hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string UserPasswordHash(string password)
        {
            var r_str = password.ToSHA256();
            return r_str;
        }

        #region --Token加解密--
        /// <summary>
        /// token加密解密Key
        /// </summary>
        protected string TokenKeyStr = "ABCDEF9876543210";

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherStr"></param>
        /// <returns></returns>
        public string TokenDecryption(string cipherStr)
        {
            return cipherStr.AESCBCDecryption(this.TokenKeyStr);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public string TokenEncryption(string sourceStr)
        {
            return sourceStr.AESCBCEncryption(this.TokenKeyStr);
        }

        /// <summary>
        /// token hash
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public string TokenHash(string tokenStr)
        {
            var r_str = tokenStr.ToSHA256();
            return r_str;
        }
        #endregion
    }
}
