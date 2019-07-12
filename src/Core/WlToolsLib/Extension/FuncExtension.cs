using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.Extension
{
    /// <summary>
    /// Func<T...>扩展方法集合
    /// </summary>
    public static class FuncExtension
    {
        #region --多播委托扩展--
        /// <summary>
        /// Func<T>无泛型参数，泛型返回值，多播委托扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<T> RegFunc<T>(this Func<T> self, Func<T> func)
        {
            if (self.IsNull())
            {
                self = func;
            }
            self += func;
            return self;
        }

        /// <summary>
        /// Func<T>1个泛型参数，泛型返回值，多播委托扩展
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<T1, TR> RegFunc<T1, TR>(this Func<T1, TR> self, Func<T1, TR> func)
        {
            if (self.IsNull())
            {
                self = func;
            }
            self += func;
            return self;
        }

        /// <summary>
        /// Func<T>2个泛型参数，泛型返回值，多播委托扩展
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<T1, T2, TR> RegFunc<T1, T2, TR>(this Func<T1, T2, TR> self, Func<T1, T2, TR> func)
        {
            if (self.IsNull())
            {
                self = func;
            }
            self += func;
            return self;
        }

        /// <summary>
        /// Func<T>3个泛型参数，泛型返回值，多播委托扩展
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Func<T1, T2, T3, TR> RegFunc<T1, T2, T3, TR>(this Func<T1, T2, T3, TR> self, Func<T1, T2, T3, TR> func)
        {
            if (self.IsNull())
            {
                self = func;
            }
            self += func;
            return self;
        }
        #endregion
    }
}
