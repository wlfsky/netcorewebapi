using System;
using System.Collections.Generic;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 树组装器接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>

    public interface ITreeBuilder<TKey, TNode, TLeaf> : IBaseTreeWorker<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
        //List<TLeaf> SourceLeafList { get; set; }
        //List<TNode> SourceNodeList { get; set; }
        //TNode TreeRoot { get; set; }

        /// <summary>
        /// 构建树结构
        /// </summary>
        void Build();
    }
}
