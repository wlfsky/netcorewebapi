using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.Expand;

namespace WL.Core.DataService
{
    /// <summary>
    /// 带事物的联合工作
    /// </summary>
    public class UniteWork : IDisposable
    {
        public IDbConnection Conn { get; set; }
        public IDbTransaction Tran { get; set; }

        public UniteWork(IDbConnection conn)
        {
            this.Conn = conn;
            if (this.Conn != null && this.Conn.State != ConnectionState.Open)
            {
                this.Conn.Open();
            }
            this.Tran = this.Conn.BeginTransaction();
        }

        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            if (this.Tran.NotNull())
            {
                this.Tran.Commit();
            }
        }

        #region IDisposable Support
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
                    if (this.Conn != null && this.Conn.State != ConnectionState.Closed)
                    {
                        this.Conn.Close();
                        if (this.Conn != null)
                        {
                            this.Conn.Dispose();
                        }
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
                this.Tran = null;
                this.Conn = null;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~UniteWork() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            try
            {
                this.Tran.Rollback();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Trace.WriteLine(ex.Message);
            }
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}
