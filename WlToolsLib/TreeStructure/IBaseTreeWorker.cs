using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    public interface IBaseTreeWorker<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
        /// <summary>
        /// 叶子源列表
        /// </summary>
        List<TLeaf> SourceLeafList { get; set; }
        /// <summary>
        /// 节点源列表
        /// </summary>
        List<TNode> SourceNodeList { get; set; }
        /// <summary>
        /// 树根
        /// </summary>
        TNode TreeRoot { get; set; }
    }
}
