﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Pagination
{
    public static class PaginationExtension
    {
        /// <summary>
        /// 分页扩展
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="self"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageShell<TData> PageForList<TData>(this IEnumerable<TData> self, int pageIndex, int pageSize, long totalCount)
        {
            if (self == null || !self.Any()) return PageShell<TData>.CreatePageData(pageSize, pageIndex, 0);
            PageShell<TData> p = PageShell<TData>.CreatePageData(pageSize, pageIndex, totalCount);
            // 分页 Skip跳过前[p.PageStartCount]条，Take获取[Size]条数据
            var pageOrder = self.Skip(p.PageStartCount).Take(p.PageSize).ToList();
            p.AddItems(pageOrder);
            return p;
        }

        public static PageShell<TData> PageForList<TData>(this IEnumerable<TData> self, int pageIndex, int pageSize)
        {
            if (self == null || !self.Any()) return PageShell<TData>.CreatePageData(pageSize, pageIndex, 0);
            PageShell<TData> p = PageShell<TData>.CreatePageData(pageSize, pageIndex, self.LongCount());
            // 分页 Skip跳过前[p.PageStartCount]条，Take获取[Size]条数据
            var pageOrder = self.Skip(p.PageStartCount).Take(p.PageSize).ToList();
            p.AddItems(pageOrder);
            return p;
        }
    }
}
