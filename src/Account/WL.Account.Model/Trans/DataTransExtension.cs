using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Model.Business;
using WL.Account.Model.DB;

namespace WL.Account.Model
{
    public static class DataTransExtension
    {

        #region --帐号信息转换--
        /// <summary>
        /// 帐号数据实体转业务实体
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static AccountModel ToBllModel(this AccountDBModel self)
        {
            AccountModel res = new AccountModel();
            res.AccountID = self.AccountID;
            res.Account = self.Account;
            res.Password = self.Password;
            res.HighPsaaword = self.HighPsaaword;
            res.Email = self.Email;
            res.OldEmail = self.OldEmail;
            res.Mobile = self.Mobile;
            res.OldMobile = self.OldMobile;
            res.RegistTime = self.RegistTime;
            res.Status = self.Status;
            res.TempPassword = self.TempPassword;
            res.TempPassOverTime = self.TempPassOverTime;
            res.TempPassUseFor = self.TempPassUseFor;
            res.TotalLoginTimes = self.TotalLoginTimes;
            res.LastLoginTime = self.LastLoginTime;
            res.ExtensionInfo = self.ExtensionInfo;
            res.Remark = self.Remark;
            res.SysRemark = self.SysRemark;
            res.UserID = self.UserID;
            res.UserName = self.UserName;
            res.NickName = self.NickName;
            res.LastNickName = self.LastNickName;
            res.NickPic = self.NickPic;
            res.RealName = self.RealName;
            res.RealFacePic = self.RealFacePic;
            res.IDCard = self.IDCard;
            res.IDCardPic = self.IDCardPic;
            res.Address = self.Address;
            res.MainPic = self.MainPic;
            return res;
        }
        #endregion
    }
}
