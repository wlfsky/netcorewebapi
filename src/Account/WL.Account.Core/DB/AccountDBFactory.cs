using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.DB.Interface;

namespace WL.Account.Core.DB
{
    public class AccountDBFactory
    {
        [Obsolete("此方法未实现具体功能")]
        public static IUserAccountDAL CreateAccountDB()
        {
            return null;
        }
    }
}
