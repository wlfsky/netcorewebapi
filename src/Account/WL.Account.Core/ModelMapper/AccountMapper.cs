using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Business;
using WL.Account.Core.DB;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.Core.ModelMapper
{
    public class AccountMapper
    {
        public static IMapper InitAllMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                //cfg.AllowNullDestinationValues = true;
                cfg.AddProfile<AccountProfile>();
                cfg.CreateMap<DataShell<AccountDBModel>, DataShell<AccountModel>>();
                cfg.CreateMap<PageShell<AccountDBModel>, PageShell<AccountModel>>();
                cfg.CreateMap<DataShell<PageShell<AccountDBModel>>, DataShell<PageShell<AccountModel>>>();
            });
            // 下面一句只能在测试代码中调用，发布时要移除
            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();
            return mapper;
        }
    }

    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            AllowNullDestinationValues = true;
            CreateMap<AccountModel, AccountDBModel>();
            CreateMap<AccountDBModel, AccountModel>().ForMember(x => x.StatusName, opt => opt.MapFrom(y => y.Status.ToString()));
        }
    }

}
