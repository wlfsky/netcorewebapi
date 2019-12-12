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
        IDataShell<PageShell<AccountDBModel>> GetPage(PageCondition<UserQueryPageCondition> condition);
        /// <summary>
        /// 获取全部用户信息，用于管理端对照提取用户关联信息使用
        /// </summary>
        /// <returns></returns>
        IDataShell<IEnumerable<AccountDBModel>> GetAllAccount();
        #endregion

        /// <summary>
        /// 插入新帐号信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> Insert(AccountDBModel user);

        #region --更新类方法--
        /// <summary>
        /// 管理员用全更新内容（不含键字段）
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> Update(AccountDBModel user);
        /// <summary>
        /// 登录后更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> UpdateAfterLogin(AccountDBModel user);
        /// <summary>
        /// 修改密码更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> UpdateModifyPassword(AccountDBModel user);
        /// <summary>
        /// 更新账户状态
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> UpdateStatus(AccountDBModel user);
        /// <summary>
        /// 设置临时密码,（TempPassword，TempPassOverTime，TempPassUseFor）
        /// 用空参数设置临时密码就是清空临时密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> SetTempPassword(AccountDBModel user);
        /// <summary>
        /// 更新昵称
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> UpdateNickName(AccountDBModel user);
        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> UpdateNickPic(AccountDBModel user);
        /// <summary>
        /// 设置真名
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> SetRealName(AccountDBModel user);
        /// <summary>
        /// 设置身份证信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> SetIDCard(AccountDBModel user);
        /// <summary>
        /// 更新用户备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> UpdateUserRemark(AccountDBModel user);
        /// <summary>
        /// 更新系统备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> UpdateSysRemark(AccountDBModel user);
        #endregion

        #region --删除类功能--
        /// <summary>
        /// 逻辑删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<int> Del(AccountDBModel user);
        #endregion

        #region --根据给定数据键提取单个用户数据--
        /// <summary>
        /// 获取 用户信息--未指定
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> Get(AccountDBModel user);
        /// <summary>
        /// 根据AccountID 获取帐号信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> GetByAccountID(AccountDBModel user);
        /// <summary>
        /// 根据帐号 Account 获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> GetByAccount(AccountDBModel user);
        /// <summary>
        /// 根据电子邮件获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> GetByEmail(AccountDBModel user);
        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> GetByMobile(AccountDBModel user);
        /// <summary>
        /// 根据核心id获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<AccountDBModel> GetByCoreID(AccountDBModel user);
        #endregion
    }
}
