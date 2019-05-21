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
using WL.Account.Model.DB;
using WL.Core.BusinessService;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.BusinessService
{
    /// <summary>
    /// 
    /// </summary>
    public class UserSystemBLL : BaseBLL, IUserSystemBLL
    {

        private IUserAccountDAL _userDAL;

        public UserSystemBLL()
        {
            RegistType();
        }

        protected override void RegistType()
        {
            Builder.RegisterType<UserAccountDAL>().As<IUserAccountDAL>();
            base.RegistType();
            //_userDAL = Container.Resolve<IUserAccountDAL>();
            _userDAL = new UserAccountBridge();
        }

        public DataShell<UserAccount> Get(UserAccount user)
        {
            var res = ReqResTransShell<UserAccount, UserAccountDBModel, UserAccountDBModel, UserAccount>(user, (rq) => _userDAL.Get(rq));
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
