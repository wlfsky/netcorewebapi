

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;
using WL.Core.DataService;
using WL.Account.Core.DB;
using WL.Account.Core.Business;
using WL.Account.Core.DB.Interface;
using WL.Account.Core.Core;

namespace WL.Account.DataService
{

    /// <summary>
    /// 数据层业务，单个,视图和事务
    /// </summary>
    public class UserAccountDAL : IUserAccountDAL
    {

        #region --查询功能--
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

        /// <summary>
        /// 获取全部用户信息，用于管理端对照提取用户关联信息使用
        /// </summary>
        /// <returns></returns>
        public DataShell<IEnumerable<AccountDBModel>> GetAllAccount()
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.GetAllAccount();
                    return result;
                }
            }
        }
        #endregion

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

        /// <summary>
        /// 更新帐号状态
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateStatus(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user, "Status=@Status", "AccountID=@AccountID", new { Status = user.Status, @AccountID = user.AccountID });
                    if (result != 1) { return $"修改状态更新影响行数异常{result}".Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }

        #region --临时密码--
        /// <summary>
        /// 设置临时密码,（TempPassword，TempPassOverTime，TempPassUseFor）
        /// 用空参数设置临时密码就是清空临时密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> SetTempPassword(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user, 
                        "TempPassword=@TempPassword, TempPassOverTime=@TempPassOverTime, TempPassUseFor=@TempPassUseFor", 
                        "AccountID=@AccountID", 
                        new { @TempPassword = user.TempPassword, 
                            @TempPassOverTime = user.TempPassOverTime, 
                            @TempPassUseFor = user.TempPassUseFor, 
                            @AccountID = user.AccountID });
                    if (result != 1) { return PubError.DBError("ERR-00200301", "SetTempPassword|设置临时密码").ToStr().Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }
        #endregion

        #region --变更昵称，头像--
        /// <summary>
        /// 更新昵称
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateNickName(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user,
                        "LastNickName=NickName, NickName=@NickName",
                        "AccountID=@AccountID",
                        new
                        {
                            @NickName = user.NickName,
                            @LastNickName = user.LastNickName,
                            @AccountID = user.AccountID
                        });
                    if (result != 1) { return PubError.DBError("ERR-00200301", "UpdateNickName|更新昵称").ToStr().Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }
        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateNickPic(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user,
                        "NickPic=NickPic",
                        "AccountID=@AccountID",
                        new
                        {
                            @NickPic = user.NickPic,
                            @AccountID = user.AccountID
                        });
                    if (result != 1) { return PubError.DBError("ERR-00200301", "UpdateNickPic|更新头像").ToStr().Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }
        #endregion

        #region --真名和身份证号码--
        /// <summary>
        /// 设置真名
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> SetRealName(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user,
                        "RealName=@RealName",
                        "AccountID=@AccountID",
                        new
                        {
                            @RealName = user.RealName,
                            @AccountID = user.AccountID
                        });
                    if (result != 1) { return PubError.DBError("ERR-00200301", "SetRealName|设置真名").ToStr().Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }

        /// <summary>
        /// 设置身份证信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> SetIDCard(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user,
                        "IDCard=@IDCard, IDCardPic=@IDCardPic",
                        "AccountID=@AccountID",
                        new
                        {
                            @IDCard = user.IDCard,
                            @IDCardPic = user.IDCardPic,
                            @AccountID = user.AccountID
                        });
                    if (result != 1) { return PubError.DBError("ERR-00200301", "SetIDCard|设置身份证信息").ToStr().Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }

        #endregion

        /// <summary>
        /// 更新用户备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateUserRemark(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user,
                        "Remark=@Remark",
                        "AccountID=@AccountID",
                        new
                        {
                            @Remark = user.Remark,
                            @AccountID = user.AccountID
                        });
                    if (result != 1) { return PubError.DBError("ERR-00200301", "UpdateUserRemark|更新用户备注").ToStr().Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }

        /// <summary>
        /// 更新系统备注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> UpdateSysRemark(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var result = dal.Update(user,
                        "SysRemark=@SysRemark",
                        "AccountID=@AccountID",
                        new
                        {
                            @SysRemark = user.SysRemark,
                            @AccountID = user.AccountID
                        });
                    if (result != 1) { return PubError.DBError("ERR-00200301", "UpdateSysRemark|更新系统备注").ToStr().Fail<AccountDBModel>(); }
                    return user.Succ();
                }
            }
        }

        #endregion

        #region --根据特定信息提取用户单个信息--

        /// <summary>
        /// 根据帐号id获取用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByAccountID(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var res = dal.GetByAccountID(user);
                    return res;
                }
            }
        }

        /// <summary>
        /// 根据帐号获取用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByAccount(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var res = dal.GetByAccount(user);
                    return res;
                }
            }
        }

        /// <summary>
        /// 根据电子邮件获取用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByEmail(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var res = dal.GetByEmail(user);
                    return res;
                }
            }
        }
        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByMobile(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var res = dal.GetByMobile(user);
                    return res;
                }
            }
        }
        /// <summary>
        /// 根据核心编号获取用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetByCoreID(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var res = dal.GetByCoreID(user);
                    return res;
                }
            }
        }

        /// <summary>
        /// 获取所有用户的简要信息，用于对照名字之类
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DataShell<AccountDBModel> GetAllUser(AccountDBModel user)
        {
            using (var conn = ConnFactory.GetUserConn())
            {
                using (var dal = new UserAccountTDAL(conn))
                {
                    var res = dal.GetByCoreID(user);
                    return res;
                }
            }
        }
        #endregion

        #endregion
    }
}
