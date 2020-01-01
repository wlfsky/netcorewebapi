using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.DataShell
{
    /// <summary>
    /// 默认的数据外壳工厂实现
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class DefaultDataShellFactory<TResult> : IDataShellFactory<TResult>
    {
        public IDataShell<TResult> CreatorDataShell()
        {
            return new DataShell<TResult>();
        }

        public IDataShell<TResult> CreatorSuccDataShell(TResult data = default(TResult))
        {
            return DataShellCreator.CreateSuccess<TResult>();
        }

        public IDataShell<TResult> CreatorFailDataShell(string info = "")
        {
            return DataShellCreator.CreateFail<TResult>(info);
        }

        public IDataShell<TResult> CreatorFailDataShell(IList<string> infos = null)
        {
            return DataShellCreator.CreateFail<TResult>(infos);
        }

        public IDataShell<TResult> CreatorFailDataShell(Exception exception = null)
        {
            return DataShellCreator.CreateFail<TResult>(exception);
        }
    }
}
