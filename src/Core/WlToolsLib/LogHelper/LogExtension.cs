using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.LogHelper
{
    public static class LogExtension
    {
        private static ILogHelper logHelper = new DefaultLogHelper();
        public static void SendToLog(this string log)
        {
            logHelper.Log(log);
        }

        public static void SendToErrorLog(this string log)
        {
            logHelper.ErrorLog(log);
        }

        public static void SendToErrorLog(this Exception ex)
        {
            logHelper.ErrorLog(ex);
        }
    }
}
