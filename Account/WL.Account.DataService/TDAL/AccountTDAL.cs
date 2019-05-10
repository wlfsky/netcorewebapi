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
using WL.Account.Model.DB;

namespace WL.Account.DataService
{
    /// <summary>
    /// 用户帐号dal
    /// 以表为单位
    /// </summary>
    public class AccountTDAL : BaseConn
    {
        public AccountTDAL(IDbConnection conn) : this(conn, null)
        {

        }

        public AccountTDAL(IDbConnection conn, IDbTransaction tran) : base(conn, tran)
        {
            // 这4个参数设置只是为了提高底层效率
            this.TableName = "WL_USERACCOUNT";
            this.ModelType = typeof(UserAccountDBModel);
            this.KeyName = "AccountID";
            this.KeyType = typeof(string);

        }



        #region --查询类方法，范例--
        /// <summary>
        /// 获取用户信息分页
        /// </summary>
        /// <returns></returns>
        public DataShell<PageShell<UserAccountDBModel>> GetPage(PageCondition<UserAccountDBModel> condition)
        {
            string conditionStr = "";
            string orderStr = $"{CreateTimeField} DESC";
            var resultPageList = GetListPaged<UserAccountDBModel>(condition.PageIndex, condition.PageSize, conditionStr, orderStr, condition.Condition);
            return resultPageList.Success();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccountDBModel> GetSingle(UserAccountDBModel user)
        {
            var result = base.Get(user);
            if (result.NotNull())
            {
                return result.Success();
            }
            return "无此用户".Fail<UserAccountDBModel>();
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<UserAccountDBModel>> GetList(UserAccountDBModel user)
        {
            var result = GetList<UserAccountDBModel>(new { @IsDel = 0 });
            return result.Success();
        }


        /// <summary>
        /// 根据用户名获取用户名和密码组合
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<UserAccountDBModel>> GetUser(UserAccountDBModel user)
        {
            var result = base.GetList<UserAccountDBModel>(new string[] { "Account", "Password" }, new { @Account = user.Account });
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
        public DataShell<UserAccountDBModel> Insert(UserAccountDBModel user)
        {
            user.AccountID = Convert.ToString(this.NewUserID().Data);
            this.Insert<int, UserAccountDBModel>(user);
            return user.Success();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccountDBModel> Update(UserAccountDBModel account)
        {
            var result = base.Update(account);
            return account.Success();
        }

        /// <summary>
        /// 自定义条件，更新自定义字段
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccountDBModel> UpdateMoble(UserAccountDBModel account)
        {
            var result = base.Update<UserAccountDBModel>(new { @Mobile = account.Mobile }, new { @AccountID = account.AccountID });
            return account.Success();
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<int> Del(UserAccountDBModel account)
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

    }
}
