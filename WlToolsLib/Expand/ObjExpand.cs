using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Expand
{
    public static class ObjExpand
    {
        #region --检查对象是否null--
        /// <summary>
        /// 检查对象是否null，是返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T self)
        {
            return ReferenceEquals(self, null);
        }

        /// <summary>
        /// 检查对象是否null 不是返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NotNull<T>(this T self)
        {
            return !self.IsNull();
        }
        #endregion --检查对象是否null--

        #region --拷贝扩展--
        /// <summary>
        /// 传送拷贝
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <param name="func_copy"></param>
        /// <returns></returns>
        public static TT TransCopy<TS, TT>(this TS self, Func<TS, TT> func_copy)
        {
            return func_copy(self);
        }

        /// <summary>
        /// 用单个转换直接转换列表数据
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <param name="sou"></param>
        /// <returns></returns>
        public static IList<TT> TransList<TS, TT>(this Func<TS, TT> self, IList<TS> sou)
        {
            var rl = new List<TT>();
            foreach (var si in sou)
            {
                rl.Add(self(si));
            }
            return rl;
        }

        /// <summary>
        /// 将单个转换委托，转换成列表转换委托
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Func<IList<TS>, IList<TT>> TransIListFunc<TS, TT>(this Func<TS, TT> self)
        {
            Func<IList<TS>, IList<TT>> tl = sl =>
            {
                var rl = new List<TT>();
                foreach (var si in sl)
                {
                    rl.Add(self(si));
                }
                return rl;
            };
            return tl;
        }

        /// <summary>
        /// 将单个转换委托，转换成列表转换委托
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <typeparam name="TT"></typeparam>
        /// <param name="self"></param>
        /// <param name="looprefunc">循环中再处理</param>
        /// <returns></returns>
        public static Func<List<TS>, List<TT>> TransListFunc<TS, TT>(this Func<TS, TT> self, Func<TT, TT> looprefunc = null)
        {
            Func<List<TS>, List<TT>> tl = sl =>
            {
                var rl = new List<TT>();
                foreach (var si in sl)
                {
                    var t1 = self(si);
                    if (looprefunc.NotNull()) { t1 = looprefunc(t1); }
                    rl.Add(t1);
                }
                return rl;
            };
            return tl;
        }

        #endregion
    }
}
