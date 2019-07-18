using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using C = System.Console;
using WlToolsLib.Extension;
using System.Reflection;
using WL.Core.Common.InterfaceMaker;

namespace WL.Core.ConsoleApp
{
    

    public class AccountReq
    {
        public string Account { get; set; }
    }

    public class AccountRes
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public List<Role> Roles { get; set; }
        public Address AddressInfo { get; set; }
    }

    public class Address
    {
        public string AddressStr { get; set; }
    }

    public class Role
    {
        public string RoleName { get; set; }
    }

    public interface IAccountTest
    {
        [ControllerRoute("/api/account/get")]
        AccountRes GetAccount(AccountReq req);
        List<AccountRes> Accounts(AccountReq req);
    }
}
