﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.Extension;

namespace WlToolsLib.Pagination
{
    /// <summary>
    /// 分页实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageShell<T>
    {
        public PageShell()
        {

        }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 实际每页记录数，最后一页记录数可能少于每页记录数
        /// </summary>
        public int ActualPageSize { get; set; }
        /// <summary>
        /// 页索引数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 当页起始记录数，为sql server分页sql语句准备的数据
        /// </summary>
        public int PageStartCount { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public long RecordCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// SQL server下，分页第一次查询的数量
        /// </summary>
        public long TopCount { get; set; }
        /// <summary>
        /// 数据队列
        /// </summary>
        public List<T> Rows { get; set; }
        /// <summary>
        /// 初始化分页
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引数</param>
        /// <param name="totalRecordCount">总记录数</param>
        protected PageShell(int pageSize, int pageIndex, long totalRecordCount)
        {
            this.PageSize = pageSize;
            this.RecordCount = totalRecordCount;
            this.PageIndex = pageIndex;
            Rows = new List<T>();
            Compute();
        }
        /// <summary>
        /// 用于快速的生成一个分页数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalRecordCount"></param>
        /// <returns></returns>
        public static PageShell<T> CreatePageData(int pageSize, int pageIndex, long totalRecordCount)
        {
            return new PageShell<T>(pageSize, pageIndex, totalRecordCount);
        }
        /// <summary>
        /// 计算分页数据
        /// </summary>
        protected virtual void Compute()
        {
            #region -- 计算分页数据 --
            PageSize = PageSize < 1 ? 20 : PageSize;//默认20
            int lastPage = Convert.ToInt32(RecordCount % PageSize);//计算最后一页记录数
            PageCount = Convert.ToInt32(RecordCount / PageSize) + (lastPage > 0 ? 1 : 0);//计算总页数
            PageIndex = PageIndex > PageCount ? PageCount : PageIndex;//检查当前页数大
            PageIndex = PageIndex < 1 ? 1 : PageIndex;//检查当前页小
            TopCount = PageIndex * PageSize;//sqlite中用的 top 多少记录数，比sql server少pagesize个
            ActualPageSize = (PageIndex == PageCount && lastPage != 0) ? lastPage : PageSize;//判断是否最后一页，并指定页记录数
            PageStartCount = (PageIndex - 1) * PageSize;//sql server用的
            #endregion -- 计算分页完成 --
        }
        /// <summary>
        /// 添加一个数据
        /// </summary>
        /// <param name="item"></param>
        public PageShell<T> AddItem(T item)
        {
            if (this.Rows.NotNull() && item.NotNull())
            {
                this.Rows.Add(item);
            }
            return this;
        }

        /// <summary>
        /// 添加一组数据
        /// </summary>
        /// <param name="items"></param>
        public PageShell<T> AddItems(IEnumerable<T> items)
        {
            if (this.Rows.NotNull() && items.NotNull())
            {
                this.Rows.AddRange(items);
            }
            return this;
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        public PageShell<T> ClearItem()
        {
            if (this.Rows.HasItem())
            {
                this.Rows.Clear();
            }
            return this;
        }
    }
}
