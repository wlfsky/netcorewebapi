// ------------------------------------
// ProjectName : WL.X.BusinessService
// FileName    : BusinessConfig
// CreateTime  : 2017/8/16 18:31:45
// Creator     : weilai
// Remark      : 
// ------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WL.Core.BusinessService
{
    /// <summary>
    /// BusinessConfig 业务层配置
    /// </summary>
    public static class BusinessConfig
    {
        /// <summary>
        /// 获取配置连接
        /// </summary>
        /// <param name="connStrName"></param>
        /// <returns></returns>
        public static string GetConfig(string connStrName)
        {
            string configFileName = Constant.BusinessConfigFile();

            var configMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
            Configuration externalConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            string result = externalConfig.AppSettings.Settings[connStrName].Value;
            //
            return result;
        }
    }
}
