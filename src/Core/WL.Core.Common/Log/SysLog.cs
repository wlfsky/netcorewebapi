using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WL.Core.Common.Log
{
    public class SysLog
    {
        public static void ErrorLog(string msg)
        {
            DebugVS(msg);
            ILog log = new Log4Api();
            log.ErrorLog(msg);
        }
        public static void DebugLog(string msg)
        {
            DebugVS(msg);
            ILog log = new Log4Api();
            log.DebugLog(msg);
        }
        public static void InfoLog(string msg)
        {
            DebugVS(msg);
            ILog log = new Log4Api();
            log.InfoLog(msg);
        }

        public static void ErrorLog(Exception ex)
        {
            DebugVS(ex);
            ILog log = new Log4Api();
            log.ErrorLog(ex);
        }
        public static void DebugLog(Exception ex)
        {
            DebugVS(ex);
            ILog log = new Log4Api();
            log.DebugLog(ex);
        }
        public static void InfoLog(Exception ex)
        {
            DebugVS(ex);
            ILog log = new Log4Api();
            log.InfoLog(ex);
        }

        private static void DebugVS(string msg)
        {
            Debug.WriteLine(msg);
        }

        private static void DebugVS(Exception ex)
        {
            Debug.WriteLine(ExceptionFormat(ex));
        }

        private static string ExceptionFormat(Exception ex)
        {
            var msg = $"Msg: {ex.Message} -- StackTrace {ex.StackTrace}";
            return msg;
        }
    }
}
