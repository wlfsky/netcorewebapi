using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 构建过滤器，继承自 元素过滤器
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    public interface IBuildFilter<TKey, TNode, TLeaf> : IElementFilter<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
    }
}
