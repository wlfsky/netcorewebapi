// ------------------------------------
// ProjectName : WL.Core.Common
// FileName    : permissionDenied
// CreateTime  : 2017/8/16 16:18:00
// Creator     : weilai
// Remark      : 
// ------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WL.Core.Common
{
    /// <summary>
    /// 权限不足异常
    /// </summary>
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException() : base("权限不足")
        {

        }
    }
}
