using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WL.Core.WebApi.Common
{
    public static class Expand
    {
        /// <summary>
        /// StringSegment 是否为空或空字符串
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NullEmpty(this StringSegment self)
        {
            return StringSegment.IsNullOrEmpty(self);
        }
    }
}
