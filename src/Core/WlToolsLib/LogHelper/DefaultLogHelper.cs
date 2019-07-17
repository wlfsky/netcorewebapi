using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WlToolsLib.LogHelper
{
    public class DefaultLogHelper : ILogHelper
    {
        public void ErrorLog(string log)
        {
            string logstr = log;
            Console.WriteLine(log);
            Debug.WriteLine(log);
        }

        public void ErrorLog(Exception ex)
        {
            string logstr = $"MSG: {ex.Message} STRACK: {ex.StackTrace}";
            Console.WriteLine(logstr);
            Debug.WriteLine(logstr);
        }

        public void Log(string log)
        {
            string logstr = log;
            Console.WriteLine(logstr);
            Debug.WriteLine(logstr);
        }
    }
}
