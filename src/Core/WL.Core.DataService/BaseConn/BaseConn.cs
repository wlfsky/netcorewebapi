using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using WlToolsLib.Extension;
//using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using WL.Core.Model;
using WL.Core.DBModel;
using WlToolsLib.DataShell;
using WlToolsLib.Pagination;

namespace WL.Core.DataService
{
    /// <summary>
    /// 基础数据库连接
    /// </summary>
    public abstract class BaseConn : IDisposable
    {
        public static string ValueHeadChar = SimpleCRUD.ValueHeadChar;

        /// <summary>
        /// dapper基础链接
        /// </summary>
        public BaseConn() : this(new MySqlConnection(), null)
        {
            //ConnStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User ID=system;Password=Wkl123456";
            //Dapper.SimpleCRUD.SetDialect(SimpleCRUD.Dialect.Oracle);
        }

        public BaseConn(IDbConnection con) : this(con, null)
        {
        }

        public BaseConn(IDbConnection con, IDbTransaction tran)
        {
            this.Con = con;
            this.Tran = tran;
            this.Com = this.Con.CreateCommand();
            this.Com.Transaction = this.Tran;
        }

        public IDbConnection Con { get; set; }
        public IDbCommand Com { get; set; }
        public IDbTransaction Tran { get; set; }
        public CommandType ComType { get; set; }
        public string ConnStr { get; protected set; }
        public int CommandTimeOut { get; set; } = 10;
        //
        public string TableName { get; set; }
        public Type ModelType { get; set; }
        public string KeyName { get; set; }
        public Type KeyType { get; set; }

        public static readonly string CreatorField = "Creator";
        public static readonly string CreateTimeField = "CreateTime";
        public static readonly string EditorField = "Editor";
        public static readonly string EditTimeField = "EditTime";
        public static readonly string IsDelField = "IsDel";
        public static readonly string DBSysDateFunc = "SYSDATE()";

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        protected void Open()
        {
            if (this.Con.State != ConnectionState.Open)
            {
                this.Con.Open();
            }
        }

        #region --基础功能，调用原生dapper功能--
        /// <summary>
        /// 获取查询到的首行首列数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <param name="comTimeOut"></param>
        /// <param name="comType"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sqlStr, object param = null)
        {
            this.Open();
            return Con.ExecuteScalar<T>(sqlStr, param);
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <param name="comTimeOut"></param>
        /// <param name="comType"></param>
        /// <returns></returns>
        public int Execute(string sqlStr, object param = null, int? comTimeOut = null, CommandType? comType = null)
        {
            this.Open();
            int result = 0;
            try
            {
                result = Con.Execute(sqlStr, param, this.Tran, comTimeOut, comType);
                return result;
            }
            catch (Exception ex)
            {
                //记录日志
                if (this.Tran != null)
                {
                    this.Tran.Rollback();
                }
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 查询获取组实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <param name="comTimeOut"></param>
        /// <param name="comType"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sqlStr, object param = null)
        {
            this.Open();
            var list = this.Con.Query<T>(sqlStr, param);
            return list;
        }

        /// <summary>
        /// 查询提取首个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="param"></param>
        /// <param name="comTimeOut"></param>
        /// <param name="comType"></param>
        /// <returns></returns>
        public T QueryFirstOrDef<T>(string sqlStr, object param = null)
        {
            this.Open();
            var result = this.Con.QueryFirstOrDefault<T>(sqlStr, param);
            return result;
        }
        #endregion

        #region --模式化代码,CRUD--
        #region --添加新记录--
        /// <summary>
        /// 插入新数据记录
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TKey Insert<TKey, T>(T obj) where T : BaseDBModel
        {
            if (obj.CoreID.NullEmpty())
            {
                throw new Exception("无核心编号");
            }
            var r = this.Con.Insert<TKey, T>(obj, this.Tran);
            return r;
        }
        #endregion

        #region --获取数据--
        /// <summary>
        /// 获取单个数据根据编号 根据[Key]特性
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T Get<TKey, T>(TKey id, T obj)
        {
            var r = this.Con.Get<T>(id, this.Tran);
            return r;
        }

        /// <summary>
        /// 获取键值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T Get<T>(T obj)
        {
            var id = this.ModelType.GetProperties().First((p) => p.Name == this.KeyName).GetValue(obj);
            var r = this.Con.Get<T>(id, this.Tran);
            return r;
        }



        public IEnumerable<T> GetByAnonymous<T>(object obj)
        {
            var r = this.Con.GetByAnonymous<T>(obj, this.Tran);
            return r;
        }

        /// <summary>
        /// 获取所有记录列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>()
        {
            var r_all = this.Con.GetList<T>(this.Tran);
            return r_all;
        }

        /// <summary>
        /// 获取列表，只提供实体条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(object whereConditions)
        {
            var r = this.Con.GetList<T>(whereConditions, this.Tran);
            return r;
        }

        /// <summary>
        /// 获取列表，提供字符串查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditionStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(string whereConditionStr, object param = null)
        {
            var r = this.Con.GetList<T>(whereConditionStr, param, this.Tran);
            return r;
        }

        /// <summary>
        /// 根据条件提取指定的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selectFields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(string[] selectFields, object param = null)
        {
            var r = this.Con.GetList<T>(selectFields, param, this.Tran);
            return r;
        }
        #endregion

        #region --分页--
        /// <summary>
        /// 获取分页数据，提供条件和排序，参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditionStr"></param>
        /// <param name="orderStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public PageShell<T> GetListPaged<T>(int pageIndex, int pageSize, string conditionStr, string orderStr, object param = null)
        {
            var rg = this.Con.GetListPaged<T>(pageIndex, pageSize, conditionStr, orderStr, param, this.Tran);
            var res = rg.Data.PageForList(pageIndex, pageSize, rg.TotalCount);
            return res;
        }
        #endregion

        #region --更新记录--
        /// <summary>
        /// 更新单个实体,通用版本，忽略字段 "IsDel", "AddUser", "AddTime", "ProjectID"
        ///  ModifyTime 字段单独处理外层无需幅值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Update<T>(T obj, IList<string> updateIgnoreField = null) where T : BaseDBModel
        {
            var defIgnoreList = new List<string> { IsDelField, CreatorField, CreateTimeField };
            if (updateIgnoreField.IsNull())
            {
                updateIgnoreField = defIgnoreList;
            }
            else
            {
                foreach (var item in defIgnoreList)
                {
                    if (updateIgnoreField.Contains(item).IsFalse())
                    {
                        updateIgnoreField.Add(item);
                    }
                }

            }
            var r = this.Con.Update<T>(obj, this.Tran, updateIgnoreField: updateIgnoreField);
            return r;
        }

        /// <summary>
        /// 指定更新字段，指定更新条件，进行更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateParam"></param>
        /// <param name="conditionParam"></param>
        /// <returns></returns>
        public int Update<T>(object updateParam, object conditionParam)
        {
            var r = this.Con.UpdateByAnonymous<T>(updateParam, conditionParam, this.Tran);
            return r;
        }

        /// <summary>
        /// 更新数据，提供更新字段，条件字段，和参数
        /// 需要忽略 "IsDel", "AddUser", "AddTime", "ProjectID" 字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="updateStr"></param>
        /// <param name="conditionStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Update<T>(T obj, string updateStr, string conditionStr, object param = null)
        {
            string sqlStr = $"UPDATE {TableName} SET {updateStr}, {EditTimeField} = {DBSysDateFunc} WHERE {conditionStr}";
            var r = this.Con.Execute(sqlStr, param, this.Tran);
            return r;
        }
        #endregion

        #region --删除记录--
        /// <summary>
        /// 物理删除列表，提供删除查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditionStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int RealDelList<T>(string conditionStr, object param = null)
        {
            var r = this.Con.DeleteList<T>(conditionStr, param, this.Tran);
            return r;
        }

        /// <summary>
        /// 逻辑删除列表，提供删除条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditionStr"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int DelList(string conditionStr, object param = null)
        {
            string sqlStr = $"UPDATE {this.TableName} SET {IsDelField}=1, {EditTimeField} = {DBSysDateFunc} WHERE {conditionStr} AND {IsDelField} = 0";
            var result = this.Con.Execute(sqlStr, param, this.Tran);
            return result;
        }

        /// <summary>
        /// 逻辑删除单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="keyName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Del<T>(T obj)
        {
            // 实体条件 传递给Delete方法，针对 [Key] Id 做逻辑删除
            var result = this.Con.Delete<T>(obj, this.Tran);
            return result;
        }

        /// <summary>
        /// 根据条件逻辑删除多个记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int DelList<T>(object condition)
        {
            var result = this.Con.DeleteList<T>(condition, this.Tran);
            return result;
        }

        /// <summary>
        /// 逻辑删除多个,用IDs删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="keyName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int DelList<TKey>(IEnumerable<TKey> ids)
        {
            string sqlStr = $"UPDATE {this.TableName} SET {IsDelField}=1, {EditTimeField} = {DBSysDateFunc} WHERE {this.KeyName} IN {ValueHeadChar}IDs AND {IsDelField} = 0";
            var result = this.Con.Execute(sqlStr, new { @IDs = ids }, this.Tran);
            return result;
        }
        #endregion

        #region --辅助方法--
        /// <summary>
        /// 获取键类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void GetKeyField<T>(T obj)
        {
            if (this.ModelType.IsNull())
            {
                this.ModelType = typeof(T);
            }
            if (this.KeyType.IsNull())
            {
                foreach (var item in this.ModelType.GetProperties())
                {
                    if (item.GetCustomAttributes(true).Any(attr => attr is KeyAttribute))
                    {
                        this.KeyType = item.GetType();
                        this.KeyName = item.Name;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 创建一个以时间为准的数字字符串ID
        /// </summary>
        /// <returns></returns>
        public string NewNumStrID()
        {
            return DateTime.Now.DateTimeID().ToString();
        }

        /// <summary>
        /// 结合给定头创建一个以时间为准的数字字符串ID
        /// </summary>
        /// <returns></returns>
        public string NewNumStrIDWithHead(string head)
        {
            return $"{head}{DateTime.Now.DateTimeID().ToString()}";
        }

        /// <summary>
        /// 创建一个Long ID
        /// </summary>
        /// <returns></returns>
        public long NewLongID()
        {
            return DateTime.Now.DateTimeID();
        }

        /// <summary>
        /// 创建给定头的时间id
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public long NewTimeIDWithHead(int head)
        {
            return DateTime.Now.DateTimeID();
        }

        /// <summary>
        /// 返回一个GUID，没有-号
        /// </summary>
        /// <returns></returns>
        public string NewGUID()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        #endregion
        #endregion

        #region --IDisposable Support 模式化释放资源--
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    if (this.Tran != null)
                    {
                        this.Tran.Dispose();
                    }
                    if (this.Com != null)
                    {
                        this.Com.Dispose();
                    }
                    if (this.Con != null && this.Con.State != ConnectionState.Closed)
                    {
                        this.Con.Close();
                        if (this.Con != null)
                        {
                            this.Con.Dispose();
                        }
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
                this.Tran = null;
                this.Com = null;
                this.Con = null;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~BaseConn() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }


}
