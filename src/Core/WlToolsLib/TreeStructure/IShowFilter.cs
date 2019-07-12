using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 显示过滤器
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    public interface IShowFilter<TKey, TNode, TLeaf> : IElementFilter<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
    }
}
