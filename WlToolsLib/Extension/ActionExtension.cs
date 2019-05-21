using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActionExtension
    {
        #region --多播委托扩展--
        /// <summary>
        /// Action<T>1个参数，无返回值，多播委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Action<T> RegAction<T>(this Action<T> self, Action<T> action)
        {
            if (self.IsNull())
            {
                self = action;
            }
            self += action;
            return self;
        }

        /// <summary>
        /// Action<T>2个参数，无返回值，多播委托
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Action<T1, T2> RegAction<T1, T2>(this Action<T1, T2> self, Action<T1, T2> action)
        {
            if (self.IsNull())
            {
                self = action;
            }
            self += action;
            return self;
        }

        /// <summary>
        /// Action<T>3个参数，无返回值，多播委托
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Action<T1, T2, T3> RegAction<T1, T2, T3>(this Action<T1, T2, T3> self, Action<T1, T2, T3> action)
        {
            if (self.IsNull())
            {
                self = action;
            }
            self += action;
            return self;
        }

        /// <summary>
        /// Action<T>4个参数，无返回值，多播委托
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Action<T1, T2, T3, T4> RegAction<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> self, Action<T1, T2, T3, T4> action)
        {
            if (self.IsNull())
            {
                self = action;
            }
            self += action;
            return self;
        }
        #endregion
    }
}
