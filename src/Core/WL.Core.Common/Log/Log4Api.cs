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
    public class Log4Api : ILog
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

        public void DebugLog(Exception ex)
        {
            Task.Run(() =>
            {
                log4net.ILog log = log4net.LogManager.GetLogger(repository.Name, "Log4Net.DebugLog");
                var msg = ExceptionFormat(ex);
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

        public void ErrorLog(Exception ex)
        {

            Task.Run(() =>
            {
                log4net.ILog log = log4net.LogManager.GetLogger(repository.Name, "Log4Net.ErrorLog");
                var msg = ExceptionFormat(ex);
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
        public void InfoLog(Exception ex)
        {
            Task.Run(() =>
            {
                log4net.ILog log = log4net.LogManager.GetLogger(repository.Name, "Log4Net.InfoLog");
                var msg = ExceptionFormat(ex);
                log.Info(msg);
            });
        }

        public static string ExceptionFormat(Exception ex)
        {
            var msg = $"Msg: {ex.Message} -- StackTrace {ex.StackTrace}";
            return msg;
        }
    }
}