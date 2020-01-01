using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Core.Common;
using WlToolsLib.DataShell;
using WlToolsLib.Extension;

namespace WL.Account.Core.Engine
{
    public class BusinessCore
    {
        protected static IContainer Container { get; set; }

        protected static ContainerBuilder Builder { get; set; }

        protected static ICryptoProvider UserFunc { get; set; }

        protected static IMapper Mapper { get; set; }

        public BusinessCore()
        {
            Builder = new ContainerBuilder();
            // 在每个模块中 注册此模块的业务层

            Builder.RegisterType<DefCryptoProvider>().As<ICryptoProvider>();

            //Container = Builder.Build();
            this.RegistType();
            //
            UserFunc = Container.Resolve<ICryptoProvider>();
        }

        protected virtual void RegistType()
        {
            Container = Builder.Build();
        }

        protected void RegistDll(Type dtype, Type itype)
        {

        }
        #region --请求和响应数据（map）--

        public TOut MapRequest<TIn, TOut>(TIn src)
        {
            return Mapper.Map<TIn, TOut>(src);
        }


        /// <summary>
        /// 相应数据转换
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public DataShell<TOut> MapResult<TIn, TOut>(DataShell<TIn> src)
        {
            if (src.Data.IsNull())
            {
                var res = new DataShell<TOut>();
                res.Code = src.Code;
                res.ExceptionList = src.ExceptionList;
                res.Info = src.Info;
                res.Infos = src.Infos;
                res.Operator = src.Operator;
                res.Status = src.Status;
                res.Success = src.Success;
                res.Time = src.Time;
                res.Version = src.Version;
                res.Data = default(TOut);
                return res;
            }
            else
            {
                return Mapper.Map<DataShell<TIn>, DataShell<TOut>>(src);
            }
        }

        public DataShell<TOut> ReqResTransShell<TIn, TInTo, TOutFrom, TOut>(TIn req, Func<TInTo, DataShell<TOutFrom>> process)
        {
            var reqTemp = MapRequest<TIn, TInTo>(req);
            var resTemp = process(reqTemp);
            var res = MapResult<TOutFrom, TOut>(resTemp);
            return res;
        }
        #endregion
    }
}
