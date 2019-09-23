using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Model.DB;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.Model.Business.Interface
{
    /// <summary>
    /// 帐号业务层
    /// </summary>
    public interface IAccountBLL
    {
        /// <summary>
        /// 根据id获取一个用户帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountModel> Get(AccountModel user);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition);
        /// <summary>
        /// 插入一个新用户帐号记录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountModel> Insert(AccountModel user);

        //DataShell<IEnumerable<UserAccount>> GetList(UserAccount user);

        //DataShell<UserAccount> Update(UserAccount user);

        //DataShell<int> Del(UserAccount user);
    }
}
