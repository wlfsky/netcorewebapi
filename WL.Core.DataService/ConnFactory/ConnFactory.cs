using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Configuration;
using WL.Core.DataService;
using WlToolsLib.Expand;

namespace WL.Core.DataService
{
    /// <summary>
    /// 链接工厂
    /// </summary>
    public class ConnFactory
    {
        /// <summary>
        /// 项目计划管理系统
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetProjectConn()
        {
            string connStr = GetConnStr("ProjectDBConnStr");
            return new SqliteConnection(connStr);
        }

        /// <summary>
        /// 项目用户系统
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetUserConn()
        {
            string connStr = GetConnStr("UserDBConnStr");
            if (connStr.Contains("{filename}"))
            {
                connStr = connStr.Replace("{filename}", Constant.CurrDir + "Data/SqliteDBFile.db3");
            }
            try
            {
                var connObj = new SqliteConnection(connStr);
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                return connObj;
            }
            catch (Exception ex)
            {
                var et01 = ex;
            }
            return new SqliteConnection(connStr);
        }

        /// <summary>
        /// Material Management System 物资管理系统连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetIOSConn()
        {
            string connStr = GetConnStr("MMSDBConnStr");
            return new SqliteConnection();
        }

        /// <summary>
        /// 获取配置连接
        /// </summary>
        /// <param name="connStrName"></param>
        /// <returns></returns>
        public static string GetConnStr(string connStrName)
        {
            string configFileName = Constant.DbConnConfigFile();
            var configMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
            Configuration externalConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            string result = externalConfig.AppSettings.Settings[connStrName].Value;
            //
            return result;
        }
    }
}
