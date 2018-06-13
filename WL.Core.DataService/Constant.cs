
#define NETCORE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.DataService
{
    /// <summary>
    /// 共用常量
    /// </summary>
    public static class Constant
    {
        /// <summary>
        /// 应用程序目录，
        /// </summary>
#if ASPNET
        public static readonly string CurrDir = AppDomain.CurrentDomain.BaseDirectory;
#elif NETCORE
        public static readonly string CurrDir = System.IO.Directory.GetCurrentDirectory() + "/";
#endif

        public static string DbConnConfigFile()
        {
            return $"{CurrDir}Configs/ConnConfig.config";
        }
    }
}
