using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WlToolsLib.Expand;

namespace WL.Core.WebApi.Common
{
    /// <summary>
    /// WebApi 扩展方法
    /// </summary>
    public static class Extension
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

        /// <summary>
        /// 通过泛型T生成一个ActionResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static ActionResult<T> ToActResult<T>(this T self)
        {
            return new ActionResult<T>(self);
        }

        /// <summary>
        /// 通过泛型T生成一个Task
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Task<T> ToTask<T>(this T self, Func<T> func = null)
        {
            if (func.IsNull())
            {
                return new Task<T>(() => self);
            }
            else
            {
                return new Task<T>(func);
            }
        }

        /// <summary>
        /// 生成一个Task:ActionResult:DataShell:T 泛型;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Task<ActionResult<DataShell<T>>> ToTaskActResult<T>(this DataShell<T> self, Func<ActionResult<DataShell<T>>> func = null)
        {
            if (func.IsNull())
            {
                return Task<ActionResult<DataShell<T>>>.Run<ActionResult<DataShell<T>>>(() => new ActionResult<DataShell<T>>(self));
                //return new Task<ActionResult<DataShell<T>>>();
            }
            else
            {
                return Task<ActionResult<DataShell<T>>>.Run<ActionResult<DataShell<T>>>(func);
                //return new Task<ActionResult<DataShell<T>>>(func);
            }
        }
    }
}
