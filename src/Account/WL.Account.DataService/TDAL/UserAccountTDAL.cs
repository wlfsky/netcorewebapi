using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WlToolsLib.Extension;
using WlToolsLib.Pagination;
using WL.Core.DataService;
using WL.Account.Core.DB;
using WL.Account.Core.Business;

namespace WL.Account.DataService
{
    /// <summary>
    /// 用户帐号dal
    /// 以表为单位
    /// </summary>
    public class UserAccountTDAL : BaseConn
    {
        public UserAccountTDAL(IDbConnection conn) : this(conn, null)
        {

        }

        public UserAccountTDAL(IDbConnection conn, IDbTransaction tran) : base(conn, tran)
        {
            // 这4个参数设置只是为了提高底层效率
            this.TableName = "WL_ACCOUNT";
            this.ModelType = typeof(AccountDBModel);
            this.KeyName = "AccountID";
            this.KeyType = typeof(string);

        }



        #region --查询类方法，范例--
        /// <summary>
        /// 获取用户信息分页
        /// </summary>
        /// <returns></returns>
        public DataShell<PageShell<AccountDBModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            DataShell<PageShell<AccountDBModel>> _res(string msg = "未知错误[GetPage]")
            {
                return msg.Fail<PageShell<AccountDBModel>>();
            }

            StringBuilder whereStr = new StringBuilder();
            if(condition.IsNull())
            {
                return _res("无必要参数");
            }
            if (condition.Condition.NotNull())
            {
                if (condition.Condition.AccountID.NotNullEmpty())
                {
                    whereStr.Append("AND AccountID = @AccountID ");
                }
                if (condition.Condition.Account.NotNullEmpty())
                {
                    whereStr.Append("AND Account = @Account ");
                }
                if (condition.Condition.NickName.NotNullEmpty())
                {
                    whereStr.Append("AND NickName = @NickName ");
                }
                if (condition.Condition.Email.NotNullEmpty())
                {
                    whereStr.Append("AND Email = @Email ");
                }
                if (condition.Condition.IDCard.NotNullEmpty())
                {
                    whereStr.Append("AND IDCard = @IDCard ");
                }
                if (condition.Condition.Mobile.NotNullEmpty())
                {
                    whereStr.Append("AND Mobile = @Mobile ");
                }
                if (condition.Condition.RealName.NotNullEmpty())
                {
                    whereStr.Append("AND RealName = @RealName ");
                }
                if (condition.Condition.UserName.NotNullEmpty())
                {
                    whereStr.Append("AND UserName = @UserName ");
                }
                if (condition.Condition.UserID.NotNullEmpty())
                {
                    whereStr.Append("AND UserID = @UserID ");
                }
                if (condition.Condition.Status.HasValue)
                {
                    whereStr.Append("AND Status = @Status ");
                }
                if (condition.Condition.RegistTimeBegin.HasValue)
                {
                    whereStr.Append("AND RegistTime >= @RegistTimeBegin ");
                }
                if (condition.Condition.RegistTimeEnd.HasValue)
                {
                    whereStr.Append("AND RegistTime <= @RegistTimeEnd ");
                }
            }
            string orderStr = $"{CreateTimeField} DESC";
            var resultPageList = GetListPaged<AccountDBModel>(condition.PageIndex, condition.PageSize, whereStr.ToString(), orderStr, condition.Condition);
            return resultPageList.Success();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetSingle(AccountDBModel user)
        {
            var result = base.Get(user);
            if (result.NotNull())
            {
                return result.Success();
            }
            return "无此用户".Fail<AccountDBModel>();
        }

        #region --根据特定标记获取单个数据--
        /// <summary>
        /// 帐号id提取帐号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByAccountID(AccountDBModel user)
        {
            var res = base.GetByAnonymousSingle<AccountDBModel>(new { @AccountID = user.AccountID });
            return res.Succ<AccountDBModel>();
        }

        /// <summary>
        /// 根据帐号提取账户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByAccount(AccountDBModel user)
        {
            var res = base.GetByAnonymousSingle<AccountDBModel>(new { @Account = user.Account });
            return res.Succ<AccountDBModel>();
        }
        /// <summary>
        /// 根据手机号找用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByMobile(AccountDBModel user)
        {
            var res = base.GetByAnonymousSingle<AccountDBModel>(new { @Mobile = user.Mobile });
            return res.Succ<AccountDBModel>();
        }
        /// <summary>
        /// 根据电子邮件
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByEmail(AccountDBModel user)
        {
            var res = base.GetByAnonymousSingle<AccountDBModel>(new { @Email = user.Email });
            return res.Succ<AccountDBModel>();
        }
        #endregion

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<AccountDBModel>> GetList(AccountDBModel user)
        {
            var result = GetList<AccountDBModel>(new { @IsDel = 0 });
            return result.Success();
        }


        /// <summary>
        /// 根据用户名获取用户名和密码组合
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<AccountDBModel>> GetUser(AccountDBModel user)
        {
            var result = base.GetList<AccountDBModel>(new string[] { "Account", "Password" }, new { @Account = user.Account });
            return result.Success();
        }

        /// <summary>
        /// 获取全部用户信息，用于管理端对照提取用户关联信息使用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<AccountDBModel>> GetAllAccount()
        {
            var result = base.GetList<AccountDBModel>(new string[] { "CoreID", "AccountID", "Account", "Email", "Mobile", "Status", "UserName", "NickName", "RealName" }, null);
            return result.Success();
        }

        #endregion

        #region --修改类型方法，范例--
        /// <summary>
        /// 创建一个UserAccount ID
        /// 使用的是oracle seq
        /// </summary>
        /// <returns></returns>
        public DataShell<long> NewUserID()
        {
            return DateTime.Now.DateTimeID().Success();
        }

        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> Insert(AccountDBModel user)
        {
            user.CoreID = this.NewCoreID();
            user.AccountID = this.NewAccountID();
            user.UserID = user.AccountID;
            this.Insert<string, AccountDBModel>(user);
            return user.Success();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> Update(AccountDBModel account)
        {
            var result = base.Update(account);
            return account.Success();
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<int> Del(AccountDBModel account)
        {
            var result = base.Del(new { @AccountID = account.AccountID });
            return result.Success();
        }

        /// <summary>
        /// 逻辑删除列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataShell<int> DelList(List<string> accountids)
        {
            var result = base.DelList<string>(accountids);
            return result.Success();

        }
        #endregion

        #region --非通用功能--
        /// <summary>
        /// 自定义条件，更新自定义字段
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateMoble(AccountDBModel account)
        {
            var result = base.Update<AccountDBModel>(new { @Mobile = account.Mobile, @OldMobile = account.OldMobile }, new { @AccountID = account.AccountID });
            return account.Success();
        }

        /// <summary>
        /// 更新帐号状态
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateStatus(AccountDBModel account)
        {
            var result = base.Update<AccountDBModel>(new { @Status = account.Status }, new { @AccountID = account.AccountID });
            return account.Success();
        }
        #endregion

    }
}
