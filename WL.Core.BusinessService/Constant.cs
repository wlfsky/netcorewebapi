// ------------------------------------
// ProjectName : WL.X.BusinessService
// FileName    : Constant
// CreateTime  : 2017/8/16 18:23:27
// Creator     : weilai
// Remark      : 
// ------------------------------------


#define NETCORE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WL.Core.BusinessService
{
    /// <summary>
    /// Constant
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

        public static string BusinessConfigFile()
        {
            return $"{CurrDir}Configs/BusinessConfig.config";
        }
    }
}
