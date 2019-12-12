using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Business;
using WL.Account.Core.Core;
using WL.Account.Core.DB;
using WL.Account.Core.DB.Interface;
using WL.Account.Core.Extension;
using WlToolsLib.CryptoHelper;
using WlToolsLib.DataShell;
using WlToolsLib.Extension;

namespace WL.Account.Core.Engine
{
    public class Account : AccountDBModel, IAccount
    {
        IUserAccountDAL _account = AccountDBFactory.CreateAccountDB();
        #region ----
        public IDataShell<IAccountData> Create()
        {
            CoreID = "";

            #region --数据验证--
            Dictionary<string, Func<bool>> check = new Dictionary<string, Func<bool>>()
            {
                ["帐号，手机号，电子邮件不可同时为空"] = () => Account.NullEmpty() && Email.NullEmpty() && Mobile.NullEmpty(),
                ["帐号格式不合法"] = () => Account.NotNullEmpty() && Account.LegalAccount().IsFalse(),
                ["无密码"] = () => Password.NullEmpty(),
            };
            var verifyRes = check.Checker();
            if (verifyRes.haveerror)
            {
                return verifyRes.info.Fail<IAccountData>();
            }
            #endregion
            Password = Password.ToKeccak224();
            RegistTime = DateTime.Now;
            CreateTime = DateTime.Now;
            EditTime = DateTime.Now;
            Status = WL.Account.Core.Enum.AccountStatus.RegistVerify;
            // 如果用户名昵称为空用 帐号手机或者邮箱填充
            if (UserName.NullEmpty() && Account.NotNullEmpty()) { UserName = Account; }
            if (UserName.NullEmpty() && Mobile.NotNullEmpty()) { UserName = Mobile.Replace(2, 9, "*"); }
            if (UserName.NullEmpty() && Email.NotNullEmpty()) { UserName = Email.Replace(Email.BetweenSinceStr("@", "."), "****"); }

            //var res = ReqResTransShell<AccountModel, AccountDBModel, AccountDBModel, AccountModel>(this, (d) => _userDAL.Insert(d));



            return null;
        }
        public IDataShell<IAccountData> Regist()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> Login()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> Logout()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> EditInfo()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> ApplyForModifyPassword()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> ModifyPassword(ModifyPasswordReq req)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region --角色权限体系--
        public void CurrRoles()
        {

        }
        public List<Role> RolsList { get; set; }

        public bool IsRootUser()
        {
            return false;
        }

        public IDataShell<IAccountData> UpdateNickName()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> UpdateNickPic()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> SetRealName()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> SetIDCard()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> UpdateUserRemark()
        {
            throw new NotImplementedException();
        }

        public IDataShell<IAccountData> UpdateSysRemark()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
