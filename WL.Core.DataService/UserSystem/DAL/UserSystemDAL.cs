

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WL.Core.Model;
using WlToolsLib.Pagination;
using WL.Core.DataCommon;

namespace WL.Core.DataService.UserSystem
{

    /// <summary>
    /// 数据层业务，单个,视图和事务
    /// </summary>
    public class UserSystemDAL : IUserSystemDAL
    {

        /// <summary>
        /// 分页提取数据
        /// </summary>
        /// <param name="userp"></param>
        /// <returns></returns>
        public DataShell<IEnumerable<UserAccount>> GetPage(PageCondition<UserAccount> userp)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.GetPage(userp);
                    return result;
                }
            }
        }

        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccount> Get(UserAccount user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.GetSingle(user);
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
        public DataShell<IEnumerable<UserAccount>> GetList(UserAccount user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
#if DEBUG
                    try
                    {
                        var result = dal.GetList(user);
                        //throw new Exception("需要捕获的人造错误");
                        return result;
                    }
                    catch(Exception ex)
                    {
                        return ex.ToFailShell<IEnumerable<UserAccount>>();
                    }
                    return new DataShell<IEnumerable<UserAccount>>();
#else
                    var result = dal.GetList(user);
                    return result;
#endif
                }
            }
            //return "未能提取到数据".Fail<IEnumerable<BimUser>>();
        }


        public DataShell<UserAccount> GetWithProject(UserAccount user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.GetWithProject(user);
                    return result;
                }
            }
            //return "未能提取到数据".Fail<BimUser>();
        }

        #region --更新类功能--
        /// <summary>
        /// 插入新纪录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccount> Insert(UserAccount user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var uid = dal.NewUserID();
                    user.UserID = Convert.ToInt32(uid.Data);
                    var result = dal.Insert(user);
                    return result;
                }
            }

        }

        /// <summary>
        /// 事务方式插入新纪录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccount> TranWork(UserAccount user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var uk = new UniteWork(conn))
                {
                    var dal = new UserAccountTDAL(conn, uk.Tran);
                    var uid = dal.NewUserID();
                    user.UserID = Convert.ToInt32(uid.Data);
                    var result = dal.Insert(user);
                    user.RealName = "XXEW";
                    result = dal.Update(user);
                    user.NickName = "XXEW11";
                    result = dal.Update(user);
                    uk.Commit();
                    return result;
                }
            }

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<UserAccount> Update(UserAccount user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user);
                    return result;
                }
            }
        }

        /// <summary>
        /// 逻辑上删除
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<int> Del(UserAccount user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Del(user);
                    return result;
                }
            }
        }

        /// <summary>
        /// 逻辑删除列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<int> DelList(UserAccount user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Del(user);
                    return result;
                }
            }
        }
        #endregion
    }
}
