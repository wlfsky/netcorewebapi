﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 分类
    /// </summary>
    public class CategoryModel : BaseModel
    {
        /// <summary>
        /// 分类id
        /// </summary>
        public string CateId { get; set; }
        /// <summary>
        /// 父级分类id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 根分类id
        /// </summary>
        public string RootId { get; set; }
        /// <summary>
        /// 唯一别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 分类id路径 /n/m 形式
        /// </summary>
        public string CidPath { get; set; }
        /// <summary>
        /// 别名路径
        /// </summary>
        public string AliasPath { get; set; }
        /// <summary>
        /// 分类标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 同级排序
        /// </summary>
        public int SortNum { get; set; }
        /// <summary>
        /// 新内容数量
        /// </summary>
        public int NewCount { get; set; }
        /// <summary>
        /// 子类数量
        /// </summary>
        public int TotalCount { get; set; }
    }
}
