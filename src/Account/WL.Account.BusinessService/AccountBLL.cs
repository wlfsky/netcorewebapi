using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using WL.Account.DataBridge;
using WL.Account.DataService;
using WL.Account.Model.Business;
using WL.Account.Model.Business.Interface;
using WL.Account.Model.DB;
using WL.Account.Model.DB.Interface;
using WL.Core.BusinessService;
using WlToolsLib.CryptoHelper;
using WlToolsLib.DataShell;
using WlToolsLib.Extension;
using WlToolsLib.Pagination;
using WL.Account.BusinessService.Common;
using WL.Account.Model;
using WL.Account.Model.Core;

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

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountModel, AccountDBModel>();
                cfg.CreateMap<AccountDBModel, AccountModel>();
                cfg.CreateMap<DataShell<AccountDBModel>, DataShell<AccountModel>>();
                cfg.CreateMap<PageShell<AccountDBModel>, PageShell<AccountModel>>();
                cfg.CreateMap<DataShell<PageShell<AccountDBModel>>, DataShell<PageShell<AccountModel>>>();
            });
            // 下面一句只能在测试代码中调用，发布时要移除
            configuration.AssertConfigurationIsValid();
            Mapper = configuration.CreateMapper();

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
        public DataShell<AccountModel> Get(AccountModel user)
        {
            var res = ReqResTransShell<AccountModel, AccountDBModel, AccountDBModel, AccountModel>(user, (rq) => _userDAL.Get(rq));
            return res;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataShell<PageShell<AccountModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
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
        public DataShell<AccountModel> Insert(AccountModel user)
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
            user.Status = Model.Enum.AccountStatus.RegistVerify;
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
        public DataShell<AccountModel> Regist(AccountModel user)
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
        public DataShell<AccountModel> Login(AccountModel user)
        {
            #region --提取用户信息--
            DataShell<AccountDBModel> tempres = _userDAL.GetByAccount(user);
            tempres = _userDAL.GetByAccountID(user);
            tempres = _userDAL.GetByMobile(user);
            tempres = _userDAL.GetByEmail(user);
            if (!tempres.Success)
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
        public DataShell<AccountModel> ApplyForModifyPassword()
        {
            throw new Exception("no function body");
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public DataShell<AccountModel> ModifyPassword(ModifyPasswordReq req)
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
            DataShell<AccountDBModel> tempuser = _userDAL.GetByAccount(user);
            tempuser = _userDAL.GetByAccountID(user);
            tempuser = _userDAL.GetByMobile(user);
            tempuser = _userDAL.GetByEmail(user);
            if (!tempuser.Success)
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
        public DataShell<string> Logout()
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
        protected static DataShell<string> PasswordVerify(string srcPass, string verifyPass)
        {
            var cyPass = CommonLib.AccountPassword(verifyPass);
            if(srcPass != cyPass)
            {
                return "用户名/手机/邮件或者密码不对".Fail<string>();
            }
            return "success".Succ();
        }
        #endregion
    }
}
