using System;
using WL.Account.BusinessBridge;
using WL.Account.BusinessService;
using WL.Account.DataBridge;
using WL.Account.DataService;
using WL.Account.Model.Business.Interface;
using WL.Account.Model.DB.Interface;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test_GetList()
        {
            //UserSystemBLL test_dal = new UserSystemBLL();
            //var r = test_dal.GetList(new WL.Account.Model.Business.UserAccount());
            //Console.WriteLine(r);
        }

        [Fact]
        public void Test_DB_GetByID()
        {
            IUserAccountDAL test_dal = new UserAccountDAL();
            var r = test_dal.Get(new WL.Account.Model.DB.AccountDBModel() { AccountID = "7" });
            Console.WriteLine(r);
        }


        [Fact]
        public void Test_DBB_GetByID()
        {
            IUserAccountDAL test_dal = new AccountDataBridge();
            var r = test_dal.Get(new WL.Account.Model.DB.AccountDBModel() { AccountID = "7" });
            Console.WriteLine(r);

            IAccountBLL ubll = new AccountBLL();
            var u = ubll.Get(new WL.Account.Model.Business.AccountModel() { AccountID = "7" });
            Console.WriteLine(u);

        }

        [Fact]
        public void Test_UserBusinessBrideg()
        {
            //
            IAccountBLL ab = new AccountBusinessBridge();
            var res = ab.Get(new WL.Account.Model.Business.AccountModel() { AccountID = "7" });
            Console.WriteLine(res);

        }
    }
}

