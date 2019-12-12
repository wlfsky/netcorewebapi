using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Core;
using WlToolsLib.DataShell;

namespace WL.Account.Core.Engine
{
    public interface IAccount : IAccountData
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        IDataShell<IAccountData> Create();
        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <returns></returns>
        IDataShell<IAccountData> Regist();


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        IDataShell<IAccountData> Login();

        /// <summary>
        /// 用户退出登录
        /// </summary>
        /// <returns></returns>
        IDataShell<IAccountData> Logout();

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <returns></returns>
        IDataShell<IAccountData> EditInfo();
        /// <summary>
        /// 申请修改密码
        /// </summary>
        /// <returns></returns>
        IDataShell<IAccountData> ApplyForModifyPassword();
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IDataShell<IAccountData> ModifyPassword(ModifyPasswordReq req);



        #region --管理功能--


        #region --变更昵称，头像--
        /// <summary>
        /// 更新昵称
        /// </summary>
        /// <returns></returns>
        IDataShell<IAccountData> UpdateNickName();
        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<IAccountData> UpdateNickPic();
        #endregion

        #region --真名和身份证号码--
        /// <summary>
        /// 设置真名
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<IAccountData> SetRealName();

        /// <summary>
        /// 设置身份证信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<IAccountData> SetIDCard();

        #endregion

        /// <summary>
        /// 更新用户备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<IAccountData> UpdateUserRemark();

        /// <summary>
        /// 更新系统备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IDataShell<IAccountData> UpdateSysRemark();
        #endregion
    }
}
