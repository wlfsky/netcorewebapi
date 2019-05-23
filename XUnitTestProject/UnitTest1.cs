using System;
using WL.Account.BusinessService;
using WL.Account.DataBridge;
using WL.Account.DataService;
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
            var r = test_dal.Get(new WL.Account.Model.DB.UserAccountDBModel() { AccountID = "7" });
            Console.WriteLine(r);
        }


        [Fact]
        public void Test_DBB_GetByID()
        {
            IUserAccountDAL test_dal = new UserAccountBridge();
            var r = test_dal.Get(new WL.Account.Model.DB.UserAccountDBModel() { AccountID = "7" });
            Console.WriteLine(r);
        }
    }
}

