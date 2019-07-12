using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Common
{
    public interface ILog4
    {
        void ErrorLog(string msg);
        void DebugLog(string msg);
        void InfoLog(string msg);
    }
}
