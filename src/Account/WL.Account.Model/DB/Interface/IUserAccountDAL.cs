using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Business;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Account.Core.DB.Interface
{
    /// <summary>
    /// 用户数据层接口
    /// </summary>
    public interface IUserAccountDAL
    {


        #region --管理后台 功能--
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataShell<PageShell<AccountDBModel>> GetPage(PageCondition<UserQueryPageCondition> condition);
        #endregion

        /// <summary>
        /// 插入新帐号信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> Insert(AccountDBModel user);

        #region --更新类方法--
        /// <summary>
        /// 管理员用全更新内容（不含键字段）
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> Update(AccountDBModel user);
        /// <summary>
        /// 登录后更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> UpdateAfterLogin(AccountDBModel user);
        /// <summary>
        /// 修改密码更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> UpdateModifyPassword(AccountDBModel user);
        #endregion

        #region --删除类功能--
        /// <summary>
        /// 逻辑删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<int> Del(AccountDBModel user);
        #endregion

        #region --根据给定数据键提取单个用户数据--
        /// <summary>
        /// 获取 用户信息--未指定
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> Get(AccountDBModel user);
        /// <summary>
        /// 根据AccountID 获取帐号信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> GetByAccountID(AccountDBModel user);
        /// <summary>
        /// 根据帐号 Account 获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> GetByAccount(AccountDBModel user);
        /// <summary>
        /// 根据电子邮件获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> GetByEmail(AccountDBModel user);
        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> GetByMobile(AccountDBModel user);
        /// <summary>
        /// 根据核心id获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        DataShell<AccountDBModel> GetByCoreID(AccountDBModel user);
        #endregion
    }
}
