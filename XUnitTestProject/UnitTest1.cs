using System;
using WL.Account.BusinessService;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test_GetList()
        {
            UserSystemBLL test_dal = new UserSystemBLL();
            var r = test_dal.GetList(new WL.Account.Model.Business.UserAccount());
            Console.WriteLine(r);
        }

        [Fact]
        public void Test_GetByID()
        {
            UserSystemBLL test_dal = new UserSystemBLL();
            var r = test_dal.Get(new WL.Account.Model.Business.UserAccount() { AccountID = "7" });
            Console.WriteLine(r);
        }
    }
}

