using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using WL.Account.DataBridge;
using WL.Account.DataService;
using WL.Account.Model.Business;
using WL.Account.Model.Business.Interface;
using WL.Account.Model.DB;
using WL.Account.Model.DB.Interface;
using WL.Core.BusinessService;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.BusinessService
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountBLL : BaseBLL, IAccountBLL
    {

        private IUserAccountDAL _userDAL;

        /// <summary>
        /// 帐号业务
        /// </summary>
        public AccountBLL()
        {
            //RegistType();
            _userDAL = new AccountDataBridge();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountModel, AccountDBModel>();
                cfg.CreateMap<AccountDBModel, AccountModel>();
                cfg.CreateMap<DataShell<AccountDBModel>, DataShell<AccountModel>>();
                cfg.CreateMap<PageShell<AccountDBModel>, PageShell<AccountModel>>();
                cfg.CreateMap<DataShell<PageShell<AccountDBModel>>, DataShell<PageShell<AccountModel>>>();
            });
            // 下面一句只能在测试代码中调用，发布时要移除
            configuration.AssertConfigurationIsValid();
            Mapper = configuration.CreateMapper();

        }

        protected override void RegistType()
        {
            Builder.RegisterType<UserAccountDAL>().As<IUserAccountDAL>();
            base.RegistType();
            //_userDAL = Container.Resolve<IUserAccountDAL>();
            _userDAL = new AccountDataBridge();
        }

        public DataShell<AccountModel> Get(AccountModel user)
        {
            var res = ReqResTransShell<AccountModel, AccountDBModel, AccountDBModel, AccountModel>(user, (rq) => _userDAL.Get(rq));
            return res;
        }

        public DataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            var res = ReqResTransShell<PageCondition<UserQueryPageCondition>, PageCondition<UserQueryPageCondition>, PageShell<AccountDBModel>, PageShell<AccountModel>>(condition, (rq) => _userDAL.GetPage(rq));
            return res;
        }

        //public DataShell<IEnumerable<UserAccount>> GetList(UserAccount user)
        //{
        //    var res = ReqResTransShell<UserAccount, UserAccountDBModel, IEnumerable<UserAccountDBModel>, IEnumerable<UserAccount>>(user, (rq) => _userDAL.GetList(rq));
        //    return res;
        //}

        //public DataShell<UserAccount> Insert(UserAccount user)
        //{
        //    var res = ReqResTransShell<UserAccount, UserAccountDBModel, UserAccountDBModel, UserAccount>(user, (d) => _userDAL.Insert(d));
        //    return res;
        //}



        //public DataShell<PageShell<UserAccount>> GetPage(PageCondition<UserAccount> userp)
        //{
        //    var res = ReqResTransShell<PageCondition<UserAccount>, PageCondition<UserAccountDBModel>, PageShell<UserAccountDBModel>, PageShell<UserAccount>>(userp, (rq) => _userDAL.GetPage(rq));
        //    return res;
        //}

        //public DataShell<UserAccount> Update(UserAccount user)
        //{
        //    var res = ReqResTransShell<UserAccount, UserAccountDBModel, UserAccountDBModel, UserAccount>(user, (rq) => _userDAL.Update(rq));
        //    return res;
        //}

        //public DataShell<int> Del(UserAccount user)
        //{
        //    var res = ReqResTransShell<UserAccount, UserAccountDBModel, int, int>(user, (rq) => _userDAL.Del(rq));
        //    return res;
        //}

    }
}
