using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.DataShell
{
    #region --返回结果扩展方法--
    /// <summary>
    /// 结果外壳扩展方法
    /// </summary>
    public static class DataShellExpand
    {
        #region --用返回类型直接构建一个 成功返回结果--

        /// <summary>
        /// 用返回类型直接构建一个 成功返回结果
        /// </summary>
        /// <typeparam name="TResult">结果实体类型</typeparam>
        /// <param name="self">扩展字符串类型</param>
        /// <returns></returns>
        public static IDataShell<TResult> Success<TResult>(this TResult self)
        {
            return DataShellCreator.CreateSuccess<TResult>(self);
        }

        /// <summary>
        /// 用返回类型直接构建一个 成功返回结果(短小版)
        /// </summary>
        /// <typeparam name="TResult">结果实体类型</typeparam>
        /// <param name="self">扩展字符串类型</param>
        /// <returns></returns>
        public static IDataShell<TResult> Succ<TResult>(this TResult self)
        {
            return DataShellCreator.CreateSuccess<TResult>(self);
        }

        #endregion --用返回类型直接构建一个 成功返回结果--

        #region --利用指定类型直接创建一个成功结果并附带一个信息--
        /// <summary>
        /// 利用指定类型直接创建一个成功结果并附带一个信息
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static IDataShell<TResult> Success<TResult>(this TResult self, string info)
        {
            return DataShellCreator.CreateSuccess<TResult>(self).Succeed(self, info);
        }

        /// <summary>
        /// 利用指定类型直接创建一个成功结果并附带一个信息(短小版)
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static IDataShell<TResult> Succ<TResult>(this TResult self, string info)
        {
            return DataShellCreator.CreateSuccess<TResult>(self).Succeed(self, info);
        }
        #endregion --利用指定类型直接创建一个成功结果并附带一个信息--

        #region --用错误信息直接构成一个错误返回信息--
        /// <summary>
        /// 用错误信息直接构成一个错误返回信息
        /// </summary>
        /// <typeparam name="TResult">结果数据实体类型</typeparam>
        /// <param name="self">扩展字符串类型</param>
        /// <returns></returns>
        public static IDataShell<TResult> Fail<TResult>(this string self)
        {
            return DataShellCreator.CreateFail<TResult>(self);
        }
        #endregion --用错误信息直接构成一个错误返回信息--

        #region --用一组错误信息初始化一个失败结果--

        /// <summary>
        /// 用一组错误信息初始化一个失败结果
        /// </summary>
        /// <typeparam name="TResult">结果实体类型</typeparam>
        /// <param name="self">扩展字符串类型</param>
        /// <returns></returns>
        public static IDataShell<TResult> Fail<TResult>(this IList<string> self)
        {
            return DataShellCreator.CreateFail<TResult>(self);
        }

        #endregion --用一组错误信息初始化一个失败结果--

        #region --用异常直接初始化一个错误结果--
        /// <summary>
        /// 根据异常信息生成一个失败的DataShell
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IDataShell<T> Fail<T>(this Exception self)
        {
            // 用错误信息创建失败，且加入堆栈信息
            return self.Message.Fail<T>().AddInfo(self.StackTrace).AddInfo(self.Source);
        }
        #endregion

        #region --用新的数据类型，完全转换一个数据外壳--
        /// <summary>
        /// 用新的Data数据类型，完全转换一个新的类型的数据外壳
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IDataShell<TTag> ToNewShell<TSrc, TTag>(this IDataShell<TSrc> self, TTag newdata = default(TTag))
        {
            IDataShell<TTag> target = new DataShell<TTag>();
            target.Code = self.Code;
            target.Data = newdata;
            target.ExceptionList = self.ExceptionList;
            target.Info = self.Info;
            target.InfoDetail = self.InfoDetail;
            target.Infos = self.Infos;
            target.Operator = self.Operator;
            target.Status = self.Status;
            target.Success = self.Success;
            target.Time = self.Time;
            target.Version = self.Version;
            return target;
        }
        #endregion
    }

#endregion --返回结果扩展方法--
}
