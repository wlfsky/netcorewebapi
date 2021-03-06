﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using WL.Account.DataBridge;
using WL.Account.DataService;
using WL.Account.Core.Business;
using WL.Account.Core.Business.Interface;
using WL.Account.Core.DB;
using WL.Account.Core.DB.Interface;
using WL.Core.BusinessService;
using WlToolsLib.CryptoHelper;
using WlToolsLib.DataShell;
using WlToolsLib.Extension;
using WlToolsLib.Pagination;
using WL.Account.BusinessService.Common;
using WL.Account.Core;
using WL.Account.Core.Core;
using WL.Account.Core.ModelMapper;

namespace WL.Account.BusinessService
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountBLL : BaseBLL, IAccountBLL
    {

        private IUserAccountDAL _userDAL;

        /// <summary>
        /// 帐号业务
        /// </summary>
        public AccountBLL()
        {
            //RegistType();
            _userDAL = new AccountDataBridge();

            Mapper = AccountMapper.InitAllMapper();

        }

        /// <summary>
        /// 注册类型
        /// </summary>
        protected override void RegistType()
        {
            Builder.RegisterType<UserAccountDAL>().As<IUserAccountDAL>();
            base.RegistType();
            //_userDAL = Container.Resolve<IUserAccountDAL>();
            _userDAL = new AccountDataBridge();
        }

        /// <summary>
        /// 获取一个用户帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> Get(AccountModel user)
        {
            var res = ReqResTransShell<AccountModel, AccountDBModel, AccountDBModel, AccountModel>(user, (rq) => _userDAL.Get(rq));
            return res;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IDataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            var res = ReqResTransShell<PageCondition<UserQueryPageCondition>, PageCondition<UserQueryPageCondition>, PageShell<AccountDBModel>, PageShell<AccountModel>>(condition, (rq) => _userDAL.GetPage(rq));
            return res;
        }

        //public DataShell<IEnumerable<UserAccount>> GetList(UserAccount user)
        //{
        //    var res = ReqResTransShell<UserAccount, UserAccountDBModel, IEnumerable<UserAccountDBModel>, IEnumerable<UserAccount>>(user, (rq) => _userDAL.GetList(rq));
        //    return res;
        //}


        #region --注册和创建--
        /// <summary>
        /// 插入新 用户帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> Insert(AccountModel user)
        {
            user.CoreID = "";

            #region --数据验证--
            Dictionary<string, Func<bool>> check = new Dictionary<string, Func<bool>>()
            {
                ["帐号，手机号，电子邮件不可同时为空"] = () => user.Account.NullEmpty() && user.Email.NullEmpty() && user.Mobile.NullEmpty(),
                ["帐号格式不合法"] = () => user.Account.NotNullEmpty() && user.Account.LegalAccount().IsFalse(),
                ["无密码"] = () => user.Password.NullEmpty(),
            };
            var verifyRes = check.Checker();
            if (verifyRes.haveerror)
            {
                return verifyRes.info.Fail<AccountModel>();
            }
            #endregion
            user.Password = user.Password.ToKeccak224();
            user.RegistTime = DateTime.Now;
            user.CreateTime = DateTime.Now;
            user.EditTime = DateTime.Now;
            user.Status = Core.Enum.AccountStatus.RegistVerify;
            // 如果用户名昵称为空用 帐号手机或者邮箱填充
            if (user.UserName.NullEmpty() && user.Account.NotNullEmpty()) { user.UserName = user.Account; }
            if (user.UserName.NullEmpty() && user.Mobile.NotNullEmpty()) { user.UserName = user.Mobile.Replace(2, 9, "*"); }
            if (user.UserName.NullEmpty() && user.Email.NotNullEmpty()) { user.UserName = user.Email.Replace(user.Email.BetweenSinceStr("@", "."), "****"); }

            var res = ReqResTransShell<AccountModel, AccountDBModel, AccountDBModel, AccountModel>(user, (d) => _userDAL.Insert(d));
            return res;
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> Regist(AccountModel user)
        {
            var res = Insert(user);
            return res;
        }
        #endregion

        //public DataShell<PageShell<UserAccount>> GetPage(PageCondition<UserAccount> userp)
        //{
        //    var res = ReqResTransShell<PageCondition<UserAccount>, PageCondition<UserAccountDBModel>, PageShell<UserAccountDBModel>, PageShell<UserAccount>>(userp, (rq) => _userDAL.GetPage(rq));
        //    return res;
        //}

        //public DataShell<UserAccount> Update(UserAccount user)
        //{
        //    var res = ReqResTransShell<UserAccount, UserAccountDBModel, UserAccountDBModel, UserAccount>(user, (rq) => _userDAL.Update(rq));
        //    return res;
        //}

        //public DataShell<int> Del(UserAccount user)
        //{
        //    var res = ReqResTransShell<UserAccount, UserAccountDBModel, int, int>(user, (rq) => _userDAL.Del(rq));
        //    return res;
        //}

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> Login(AccountModel user)
        {
            #region --提取用户信息--
            IDataShell<AccountDBModel> tempres = new DataShell<AccountDBModel>();
            switch (user)
            {
                case AccountModel u when user.Account.NotNullEmpty():
                    tempres = _userDAL.GetByAccount(user);
                    break;
                case AccountModel u when user.Email.NotNullEmpty():
                    tempres = _userDAL.GetByEmail(user);
                    break;
                case AccountModel u when user.Mobile.NotNullEmpty():
                    tempres = _userDAL.GetByMobile(user);
                    break;
                case AccountModel u when user.AccountID.NotNullEmpty():
                    tempres = _userDAL.GetByAccountID(user);
                    break;
                default:
                    throw new Exception("没有找到匹配的用户");
            }
            if (tempres.Failure)
            {
                return tempres.ToNewShell<AccountDBModel, AccountModel>();
            }
            #endregion

            #region --密码核验--
            var srcuser = tempres.Data;
            var verifyRes = PasswordVerify(srcuser.Password, user.Password);
            if (verifyRes.Failure)
            {
                return verifyRes.Info.Fail<AccountModel>();
            }
            #endregion

            #region --更新登录信息--
            user.LastLoginTime = DateTime.Now;
            user.TotalLoginTimes++;
            var update_res = _userDAL.UpdateAfterLogin(user);
            #endregion

            #region --用户权限--
            #endregion

            var res = user.ToBllModel();

            return res.Succ();
        }

        #region --修改密码功能--
        // 发起修改密码申请，应用密码修改
        public IDataShell<AccountModel> ApplyForModifyPassword()
        {
            throw new Exception("no function body");
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public IDataShell<AccountModel> ModifyPassword(ModifyPasswordReq req)
        {
            #region --早期验证--
            // 这是一个数据验证扩展的样例
            Dictionary<string, Func<bool>> check = new Dictionary<string, Func<bool>>()
            {
                ["旧密码为空"] = () => req.OldPassword.NullEmpty(),
                ["新密码两次输入不一致"] = () => req.NewPassword != req.NewPassword2,
            };
            foreach (var c in check.CheckerYield())
            {
                if (c.haveerror)
                {
                    return c.info.Fail<AccountModel>();
                }
            }
            #endregion

            #region --提取用户信息--
            AccountModel user = new AccountModel() { 
                Account = req.Account, 
                Email = req.Email, 
                Mobile = req.Mobile, 
                AccountID = req.AccountID };
            IDataShell<AccountDBModel> tempuser = new DataShell<AccountDBModel>();
            switch (user)
            {
                case AccountModel u when user.Account.NotNullEmpty():
                    tempuser = _userDAL.GetByAccount(user);
                    break;
                case AccountModel u when user.Email.NotNullEmpty():
                    tempuser = _userDAL.GetByEmail(user);
                    break;
                case AccountModel u when user.Mobile.NotNullEmpty():
                    tempuser = _userDAL.GetByMobile(user);
                    break;
                case AccountModel u when user.AccountID.NotNullEmpty():
                    tempuser = _userDAL.GetByAccountID(user);
                    break;
                default:
                    throw new Exception("没有找到匹配的用户");
            }
            tempuser = _userDAL.GetByEmail(user);
            if (tempuser.Failure)
            {
                return tempuser.ToNewShell<AccountDBModel, AccountModel>();
            }
            #endregion

            #region --密码核验--
            var srcuser = tempuser.Data;
            var verifyRes = PasswordVerify(srcuser.Password, req.OldPassword);
            if (verifyRes.Failure)
            {
                return verifyRes.Info.Fail<AccountModel>();
            }
            #endregion

            #region --更新密码--
            user.Password = CommonLib.AccountPassword(req.NewPassword);//散列加密
            var up_res = _userDAL.UpdateModifyPassword(user);//更新密码
            up_res.Data.Password = "";//移除敏感数据
            var res = up_res.ToNewShell<AccountDBModel, AccountModel>();
            #endregion

            return res;
        }
        #endregion

        #region --退出登录--
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public IDataShell<string> Logout()
        {
            throw new Exception();
        }
        #endregion

        #region --辅助功能--
        /// <summary>
        /// 密码核验
        /// </summary>
        /// <param name="srcPass">原始密码，及库内加密后的密码</param>
        /// <param name="verifyPass">要验证的密码，及未加密的密码</param>
        /// <returns></returns>
        protected static IDataShell<string> PasswordVerify(string srcPass, string verifyPass)
        {
            var cyPass = CommonLib.AccountPassword(verifyPass);
            if(srcPass != cyPass)
            {
                return "用户名/手机/邮件或者密码不对".Fail<string>();
            }
            return "success".Succ();
        }
        #endregion

        #region --角色权限体系--
        public List<Role> RolsList { get; set; }

        public bool IsRootUser()
        {
            return false;
        }
        #endregion
    }


    public class UserAction<T>
    {
        public UserAction(T act)
        {
            this.DoAction = act;
        }
        public T DoAction { get; private set; }
    }
}
