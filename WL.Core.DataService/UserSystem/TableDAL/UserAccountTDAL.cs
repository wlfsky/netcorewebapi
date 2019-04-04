using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WlToolsLib.Extension;
using WL.Core.Model;
using WlToolsLib.Pagination;

namespace WL.Core.DataService.UserSystem
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
            this.ModelType = typeof(UserAccount);
            this.KeyName = "UserID";
            this.KeyType = typeof(int);

        }



        #region --查询类方法，范例--
        /// <summary>
        /// 获取用户信息分页
        /// </summary>
        /// <returns></returns>
        public DataShell<IEnumerable<UserAccount>> GetPage(PageCondition<UserAccount> condition)
        {
            string conditionStr = "";
            string orderStr = "AddTime DESC";
            var resultPageList = GetListPaged<UserAccount>(condition.PageIndex, condition.PageSize, conditionStr, orderStr, condition.Condition);
            return resultPageList.Success();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccount> GetSingle(UserAccount user)
        {
            var result = base.Get(user);
            if (result.NotNull())
            {
                return result.Success();
            }
            return "无此用户".Fail<UserAccount>();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccount> GetWithProject(UserAccount user)
        {
            var result = base.GetByAnonymous<UserAccount>(new { @UserName = user.UserName, @CoreID = user.CoreID });
            if (result.HasItem())
            {
                return result.FirstOrDefault().Success();
            }
            return "无此用户".Fail<UserAccount>();
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<UserAccount>> GetList(UserAccount user)
        {
            var result = GetList<UserAccount>(new { @IsDel = 0 });
            return result.Success();
        }


        /// <summary>
        /// 根据用户名获取用户名和密码组合
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<UserAccount>> GetUser(UserAccount user)
        {
            var result = base.GetList<UserAccount>(new string[] { "UserName", "Password" }, new { @UserName = user.UserName });
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
        public DataShell<UserAccount> Insert(UserAccount user)
        {
            user.UserID = Convert.ToInt32(this.NewUserID().Data);
            this.Insert<int, UserAccount>(user);
            return user.Success();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccount> Update(UserAccount user)
        {
            var result = base.Update(user);
            return user.Success();
        }

        /// <summary>
        /// 自定义条件，更新自定义字段
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccount> UpdateMoble(UserAccount user)
        {
            var result = base.Update<UserAccount>(new { @Mobile = user.Mobile }, new { @UserID = user.UserID });
            return user.Success();
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<int> Del(UserAccount user)
        {
            var result = base.Del(new { @UserID = user.UserID });
            return result.Success();
        }

        /// <summary>
        /// 逻辑删除列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataShell<int> DelList(object condition)
        {
            var result = base.DelList<UserAccount>(condition);
            return result.Success();

        }
        #endregion

    }
}
