using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using log4net.Repository;
using log4net.Config;
using log4net;

namespace WL.Core.Common
{
    public class Log4Api : ILog4
    {
        private ILoggerRepository repository;
        public Log4Api()
        {
            repository = LogManager.CreateRepository("NETCoreRepository");
            // 默认简单配置，输出至控制台
            BasicConfigurator.Configure(repository);

        }
        public void DebugLog(string msg)
        {
            Task.Run(() =>
            {
                log4net.ILog log = log4net.LogManager.GetLogger(repository.Name, "Log4Net.DebugLog");
                log.Debug(msg);
            });
        }

        public void ErrorLog(string msg)
        {

            Task.Run(() =>
            {
                log4net.ILog log = log4net.LogManager.GetLogger(repository.Name, "Log4Net.ErrorLog");
                log.Error(msg);
            });
        }

        public void InfoLog(string msg)
        {
            Task.Run(() =>
            {
                log4net.ILog log = log4net.LogManager.GetLogger(repository.Name, "Log4Net.InfoLog");
                log.Info(msg);
            });
        }
    }
}