using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Core;
using WL.Account.Core.DB;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.Core.Business.Interface
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
        IDataShell<AccountModel> Get(AccountModel user);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IDataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition);
        /// <summary>
        /// 插入一个新用户帐号记录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountModel> Insert(AccountModel user);

        //DataShell<IEnumerable<UserAccount>> GetList(UserAccount user);

        //DataShell<UserAccount> Update(UserAccount user);

        //DataShell<int> Del(UserAccount user);

        /// <summary>
        /// 登录功能
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountModel> Regist(AccountModel user);
        /// <summary>
        /// 登录功能
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountModel> Login(AccountModel user);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IDataShell<AccountModel> ModifyPassword(ModifyPasswordReq req);


    }
}
