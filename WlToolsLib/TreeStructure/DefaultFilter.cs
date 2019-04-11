using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 默认过滤器，都返回true，表示不过滤
    /// </summary>
    /// <typeparam name="TKey">键值泛型</typeparam>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    public class DefaultElementFilter<TKey, TNode, TLeaf> : IElementFilter<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
        /// <summary>
        /// 元素过滤器，叶子过滤器，返回false时表示进行过滤操作（不进行该节点和节点以下子节点的动作（显示或者构建））
        /// </summary>
        /// <param name="TLeaf">要检测的叶子节点值</param>
        /// <returns></returns>
        public bool FilterLeaf(TLeaf leaf)
        {
            return true;
        }
        /// <summary>
        /// 元素过滤器，节点过滤器，返回false时表示进行过滤操作（不进行该节点和节点以下子节点的动作（显示或者构建））
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool FilterNode(TNode node)
        {
            return true;
        }
    }

    public class DefaultBuildFilter<TKey, TNode, TLeaf> : DefaultElementFilter<TKey, TNode, TLeaf>, IBuildFilter<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
    }

    public class DefaultShowFilter<TKey, TNode, TLeaf> : DefaultElementFilter<TKey, TNode, TLeaf>, IShowFilter<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
    }
}
