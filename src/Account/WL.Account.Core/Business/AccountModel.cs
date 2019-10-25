using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.DB;

namespace WL.Account.Core.Business
{
    public class AccountModel : AccountDBModel
    {
        /// <summary>
        /// 状态值文字
        /// </summary>
        public string StatusName { get; set; } = "";
    }
}
