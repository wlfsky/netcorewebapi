using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.LogHelper
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogHelper
    {
        void Log(string log);
        void ErrorLog(string log);

        void ErrorLog(Exception ex);
    }
}
