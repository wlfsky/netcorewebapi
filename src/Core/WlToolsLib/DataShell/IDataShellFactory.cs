using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.DataShell
{
    /// <summary>
    /// 数据外壳创建工厂
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IDataShellFactory<TResult>
    {
        /// <summary>
        /// 创建一个默认的数据外壳
        /// </summary>
        /// <returns></returns>
        IDataShell<TResult> CreatorDataShell()
        {
            return new DataShell<TResult>();
        }

        /// <summary>
        /// 创建一个成功的数据外壳
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IDataShell<TResult> CreatorSuccDataShell(TResult data = default(TResult))
        {
            return DataShellCreator.CreateSuccess<TResult>();
        }

        /// <summary>
        /// 创建一个失败的数据外壳，带入一个错误信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IDataShell<TResult> CreatorFailDataShell(string info = "")
        {
            return DataShellCreator.CreateFail<TResult>(info);
        }

        /// <summary>
        /// 创建一个失败的数据外壳，带入一组错误信息
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        IDataShell<TResult> CreatorFailDataShell(IList<string> infos = null)
        {
            return DataShellCreator.CreateFail<TResult>(infos);
        }

        /// <summary>
        /// 创建一个失败的数据外壳，带入异常信息
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        IDataShell<TResult> CreatorFailDataShell(Exception exception = null)
        {
            return DataShellCreator.CreateFail<TResult>(exception);
        }
    }

    
}
