

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
        /// <param name="accountp"></param>
        /// <returns></returns>
        public DataShell<PageShell<UserAccountDBModel>> GetPage(PageCondition<UserAccountDBModel> accountp)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new AccountTDAL(conn))
                {
                    var result = dal.GetPage(accountp);
                    return result;
                }
            }
        }

        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<UserAccountDBModel> Get(UserAccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new AccountTDAL(conn))
                {
                    var result = dal.GetSingle(account);
                    return result;
                }
            }
            //return "未能提取到数据".Fail<BimUser>();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<UserAccountDBModel>> GetList(UserAccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new AccountTDAL(conn))
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
                        return ex.Fail<IEnumerable<UserAccountDBModel>>();
                    }
                    return new DataShell<IEnumerable<UserAccountDBModel>>();
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
        public DataShell<UserAccountDBModel> Insert(UserAccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new AccountTDAL(conn))
                {
                    var uid = dal.NewUserID();
                    account.AccountID = Convert.ToString(uid.Data);

                    var result = dal.Insert(account);
                    return result;
                }
            }

        }

        /// <summary>
        /// 事务方式插入新纪录
        /// 事务范例
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<UserAccountDBModel> TranWork(UserAccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())//打开链接
            {
                using (var uk = new UniteWork(conn))//启动事务
                {
                    var dal = new AccountTDAL(conn, uk.Tran);
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

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<UserAccountDBModel> Update(UserAccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new AccountTDAL(conn))
                {
                    var result = dal.Update(account);
                    return result;
                }
            }
        }

        /// <summary>
        /// 逻辑上删除
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataShell<int> Del(UserAccountDBModel account)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new AccountTDAL(conn))
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
                using (var dal = new AccountTDAL(conn))
                {
                    var result = dal.DelList(accountids);//要测试是否可行
                    return result;
                }
            }
        }
        #endregion
    }
}
