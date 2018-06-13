#define NETCORE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WL.Core.WebApi.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class Constant
    {
        public static string RNNewline = "\r\n";

        public static string NNewline = "\n";

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
