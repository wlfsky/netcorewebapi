using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Common
{
    public interface ILog
    {
        void ErrorLog(string msg);
        void ErrorLog(Exception ex);
        void DebugLog(string msg);
        void DebugLog(Exception ex);
        void InfoLog(string msg);
        void InfoLog(Exception ex);
    }
}
