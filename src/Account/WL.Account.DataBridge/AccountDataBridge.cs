using System;
using WL.Account.DataService;
using WL.Account.Core.Business;
using WL.Account.Core.DB;
using WL.Account.Core.DB.Interface;
using WL.Core.InterfaceBridge.InterfaceBridge;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;
using System.Collections.Generic;
using WL.Core.Common.Log;

namespace WL.Account.DataBridge
{
    public class AccountDataBridge : IUserAccountDAL
    {
        IServiceBridge bridge;
        public AccountDataBridge()
        {
            bridge = new WebApiServiceBridge();
            bridge.Version = 1;
            bridge.ServiceUrlMaker = new AccountDataUrlMaker();
        }

        public IDataShell<int> Del(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/Del";
            var res = bridge.CallApi<AccountDBModel, DataShell<int>>(funcUrl, user);
            return res;
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IDataShell<PageShell<AccountDBModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            string consoleLogStr = "call data_service/useraccount/getpage";
            SysLog.InfoLog(consoleLogStr);
            string funcUrl = "/api/UserAccount/GetPage";
            var res = bridge.CallApi<PageCondition<UserQueryPageCondition>, DataShell<PageShell<AccountDBModel>>>(funcUrl, condition);
            return res;
        }
        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> Get(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/get";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 插入新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> Insert(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/Insert";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }

        #region --更新功能--
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> Update(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/Update";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 登录后更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> UpdateAfterLogin(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/UpdateAfterLogin";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 修改密码更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> UpdateModifyPassword(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/UpdateModifyPassword";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        #endregion

        #region --获取功能--
        /// <summary>
        /// 根据帐号id获取帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> GetByAccountID(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByAccountID";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 根据帐号获取帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> GetByAccount(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByAccount";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 根据电子邮件获取帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> GetByEmail(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByEmail";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 根据移动电话
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> GetByMobile(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByMobile";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 根据核心id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> GetByCoreID(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/GetByCoreID";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 更新帐号状态
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> UpdateStatus(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/UpdateStatus";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 设置临时密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> SetTempPassword(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/SetTempPassword";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 更新昵称
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> UpdateNickName(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/UpdateNickName";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 更新昵称
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> UpdateNickPic(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/UpdateNickPic";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 设置真名
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> SetRealName(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/SetRealName";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 设置身份信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> SetIDCard(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/SetIDCard";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 更新用户备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> UpdateUserRemark(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/UpdateUserRemark";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 更新系统备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountDBModel> UpdateSysRemark(AccountDBModel user)
        {
            string funcUrl = "/api/UserAccount/UpdateSysRemark";
            var res = bridge.CallApi<AccountDBModel, DataShell<AccountDBModel>>(funcUrl, user);
            return res;
        }
        /// <summary>
        /// 获取全部用户信息，用于管理端对照提取用户关联信息使用
        /// </summary>
        /// <returns></returns>
        public IDataShell<IEnumerable<AccountDBModel>> GetAllAccount()
        {
            string funcUrl = "/api/UserAccount/GetAllAccount";
            var res = bridge.CallApi<AccountDBModel, DataShell<IEnumerable<AccountDBModel>>>(funcUrl, null);
            return res;
        }
        #endregion
    }

    
}
