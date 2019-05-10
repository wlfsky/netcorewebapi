using System;
using System.Collections.Generic;
using WlToolsLib.Extension;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 基类节点
    /// </summary>
    public interface BaseNode<TKey> : BaseLeaf<TKey>
    {
        /// <summary>
        /// 子节点
        /// </summary>
        List<BaseLeaf<TKey>> ChildrenNodes { get; set; }
    }
}
