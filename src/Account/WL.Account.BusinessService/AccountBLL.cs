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
using WlToolsLib.CryptoHelper;
using WlToolsLib.DataShell;
using WlToolsLib.Extension;
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

        /// <summary>
        /// 获取一个用户帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountModel> Get(AccountModel user)
        {
            var res = ReqResTransShell<AccountModel, AccountDBModel, AccountDBModel, AccountModel>(user, (rq) => _userDAL.Get(rq));
            return res;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 插入新 用户帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountModel> Insert(AccountModel user)
        {
            user.CoreID = "";
            if (user.Account.NullEmpty() && user.Mobile.NullEmpty() && user.Email.NullEmpty())
            {
                return "帐号，手机号，电子邮件不可同时为空".Fail<AccountModel>();
            }
            if (user.Password.NullEmpty())
            {
                return "无密码".Fail<AccountModel>();
            }
            user.Password = user.Password.ToKeccak224();
            user.RegistTime = DateTime.Now;
            user.CreateTime = DateTime.Now;
            user.EditTime = DateTime.Now;
            user.Status = Model.Enum.AccountStatus.RegistVerify;
            // 如果用户名昵称为空用 帐号手机或者邮箱填充
            if (user.UserName.NullEmpty() && user.Account.NotNullEmpty()) { user.UserName = user.Account; }
            if (user.UserName.NullEmpty() && user.Mobile.NotNullEmpty()) { user.UserName = user.Mobile.Replace(2, 9, "*"); }
            if (user.UserName.NullEmpty() && user.Email.NotNullEmpty()) { user.UserName = user.Email.Replace(user.Email.BetweenSinceStr("@", "."), "****"); }

            var res = ReqResTransShell<AccountModel, AccountDBModel, AccountDBModel, AccountModel>(user, (d) => _userDAL.Insert(d));
            return res;
        }



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
