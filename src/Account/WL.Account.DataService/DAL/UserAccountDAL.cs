

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;
using WL.Core.DataService;
using WL.Account.Model.DB;
using WL.Account.Model.Business;
using WL.Account.Model.DB.Interface;

namespace WL.Account.DataService
{

    /// <summary>
    /// 数据层业务，单个,视图和事务
    /// </summary>
    public class UserAccountDAL : IUserAccountDAL
    {

        /// <summary>
        /// 分页提取数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataShell<PageShell<AccountDBModel>> GetPage(PageCondition<UserQueryPageCondition> condition)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.GetPage(condition);
                    return result;
                }
            }
        }

        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> Get(AccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.GetSingle(account);
                    return result;
                }
            }
            //return "未能提取到数据".Fail<BimUser>();
        }

        public DataShell<AccountDBModel> Login(AccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            using (var dal = new UserAccountTDAL(conn))
            {
                var result = dal.GetSingle(account);
                return result;
            }
                
            
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<AccountDBModel>> GetList(AccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
#if DEBUG
                    try
                    {
                        var result = dal.GetList(account);
                        //throw new Exception("需要捕获的人造错误");
                        return result;
                    }
                    catch(Exception ex)
                    {
                        return ex.Fail<IEnumerable<AccountDBModel>>();
                    }
                    return new DataShell<IEnumerable<AccountDBModel>>();
#else
                    var result = dal.GetList(account);
                    return result;
#endif
                }
            }
            //return "未能提取到数据".Fail<IEnumerable<BimUser>>();
        }

        #region --更新类功能--
        /// <summary>
        /// 插入新纪录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> Insert(AccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var uid = dal.NewUserID();
                    account.AccountID = Convert.ToString(uid.Data);

                    var result = dal.Insert(account);
                    return result;
                }
            }

        }

        /// <summary>
        /// 高级 管理 更新
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> Update(AccountDBModel account)
        {
            var ig_list = new List<string>() { "AccountID", "CoreID", "UserID", "Account" };
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(account, updateIgnoreField: ig_list);

                    if (result != 1) { throw new Exception("高级更新失败影响数据为0"); }
                    return account.Succ();
                }
            }
        }

        // 登录更新（查询验证，更新登录时间，次数）
        // 改密码更新
        // 改高级密码更新
        // 更新状态
        // 更新邮件
        // 更新手机号
        // 设置临时密码
        // 验证临时密码
        // 改昵称
        // 设置真名，身份证
        // 

        // 管理系统 改状态 更新
        // 

        /// <summary>
        /// 逻辑上删除
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<int> Del(AccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Del(account);
                    return result;
                }
            }
        }

        /// <summary>
        /// 逻辑删除列表通过ids
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<int> DelList(List<string> accountids)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.DelList(accountids);//要测试是否可行
                    return result;
                }
            }
        }
        #endregion

        #region --事务类更新--
        /// <summary>
        /// 事务方式插入新纪录
        /// 事务范例
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> TranWork(AccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())//打开链接
            {
                using (var uk = new UniteWork(conn))//启动事务
                {
                    var dal = new UserAccountTDAL(conn, uk.Tran);
                    var uid = dal.NewUserID();
                    account.AccountID = Convert.ToString(uid.Data);
                    var result = dal.Insert(account);
                    account.Account = "XXEW";
                    result = dal.Update(account);
                    account.Email = "XXEW11";
                    result = dal.Update(account);
                    uk.Commit();//成功就提交事务
                    return result;
                }
            }

        }

        #region --更新类功能--
        /*更新有几类：更新到外部来的新值，依据旧值更新如累加，更新其他旧值*/
        /// <summary>
        /// 登录后更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateAfterLogin(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user, "TotalLoginTimes=TotalLoginTimes+1, LastLoginTime=@LastLoginTime", "AccountID=@AccountID", new { @AccountID = user.AccountID, @LastLoginTime = user.LastLoginTime });
                    if (result != 1) { return $"登录更新影响行数异常{result}".Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }

        /// <summary>
        /// 修改密码更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateModifyPassword(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user, "Password=@Password", "AccountID=@AccountID", new { @AccountID = user.AccountID, @Password = user.Password });
                    if (result != 1) { return $"修改密码更新影响行数异常{result}".Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }
        #endregion
        #region --根据特定信息提取用户单个信息--

        public DataShell<AccountDBModel> GetByAccountID(AccountDBModel user)
        {
            throw new NotImplementedException();
        }

        public DataShell<AccountDBModel> GetByAccount(AccountDBModel user)
        {
            throw new NotImplementedException();
        }

        public DataShell<AccountDBModel> GetByEmail(AccountDBModel user)
        {
            throw new NotImplementedException();
        }

        public DataShell<AccountDBModel> GetByMobile(AccountDBModel user)
        {
            throw new NotImplementedException();
        }

        public DataShell<AccountDBModel> GetByCoreID(AccountDBModel user)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
