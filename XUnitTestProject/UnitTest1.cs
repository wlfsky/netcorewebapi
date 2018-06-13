using System;
using WL.Core.BusinessService;
//using WL.Core.DataService.UserSystem;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test_GetList()
        {
            UserSystemBLL test_dal = new UserSystemBLL();
            var r = test_dal.GetList(new WL.Core.Model.UserAccount());
            Console.WriteLine(r);
        }

        [Fact]
        public void Test_GetByID()
        {
            UserSystemBLL test_dal = new UserSystemBLL();
            var r = test_dal.Get(new WL.Core.Model.UserAccount() { UserID = 7 });
            Console.WriteLine(r);
        }
    }
}

