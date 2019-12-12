using System;
using System.Collections.Generic;
using System.Text;
using WL.Account.Core.Business;
using WL.Account.Core.DB;
using WlToolsLib.DataShell;
using WlToolsLib.Extension;

namespace WL.Account.Core
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

        /// <summary>
        /// 相应数据转换
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static IDataShell<TOut> MapResult<TIn, TOut>(IDataShell<TIn> src)
        {
            if (src.Data.IsNull())
            {
                var res = new DataShell<TOut>();
                res.Code = src.Code;
                res.ExceptionList = src.ExceptionList;
                res.Info = src.Info;
                res.Infos = src.Infos;
                res.Operator = src.Operator;
                res.Status = src.Status;
                res.Success = src.Success;
                res.Time = src.Time;
                res.Version = src.Version;
                res.Data = default(TOut);
                return res;
            }
            else
            {
                return null;// return Mapper.Map<DataShell<TIn>, DataShell<TOut>>(src);
            }
        }

        public static IDataShell<TOut> ReqResTransShell<TIn, TInTo, TOutFrom, TOut>(TIn req, Func<TInTo, DataShell<TOutFrom>> process)
        {
            var reqTemp = default(TInTo); // MapRequest<TIn, TInTo>(req);
            var resTemp = process(reqTemp);
            var res = MapResult<TOutFrom, TOut>(resTemp);
            return res;
        }
    }
}
