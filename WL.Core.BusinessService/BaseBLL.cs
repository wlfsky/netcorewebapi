using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Core.DataService;
using WL.Core.Common;
using WL.Core.DataService.UserSystem;

namespace WL.Core.BusinessService
{
    /// <summary>
    /// 基础业务类，包含ioc和部分公用功能
    /// </summary>
    public class BaseBLL
    {
        protected static IContainer Container { get; set; }

        protected static ICryptoProvider UserFunc { get; set; }

        public BaseBLL()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserSystemDAL>().As<IUserSystemDAL>();
            builder.RegisterType<DefCryptoProvider>().As<ICryptoProvider>();

            Container = builder.Build();
            //
            UserFunc = Container.Resolve<ICryptoProvider>();
        }


    }
}
