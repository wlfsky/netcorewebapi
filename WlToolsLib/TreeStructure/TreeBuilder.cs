using System;
using System.Collections.Generic;
using System.Linq;
using WlToolsLib.Extension;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 树组装器 实现
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    public class TreeBuilder<TKey, TLeaf, TNode> : ITreeBuilder<TKey, TLeaf, TNode>
        where TLeaf : BaseLeaf<TKey>
        where TNode : BaseNode<TKey>
    {
        /// <summary>
        /// 叶子源
        /// </summary>
        public List<TLeaf> SourceLeafList { get; set; }
        /// <summary>
        /// 节点源
        /// </summary>
        public List<TNode> SourceNodeList { get; set; }
        /// <summary>
        /// 树根
        /// </summary>
        public TNode TreeRoot { get; set; }
        /// <summary>
        /// 建造过滤器
        /// </summary>
        public IBuildFilter<TKey, TLeaf, TNode> BuildFilter { get; set; }


        public TreeBuilder()
        {
            if (BuildFilter == null)
            {
                BuildFilter = new DefaultBuildFilter<TKey, TLeaf, TNode>();
            }
        }

        /// <summary>
        /// 将列表构建成树结构
        /// </summary>
        public void Build()
        {
            TreeRoot.Deep = 1;
            BindNodeLeaf(TreeRoot, SourceNodeList, SourceLeafList);
        }
        /// <summary>
        /// 迭代指定节点下的节点
        /// </summary>
        /// <param name="parentNode">指定节点</param>
        /// <param name="sourceNodeList">数据源</param>
        /// <returns>迭代返回值</returns>
        private IEnumerable<TNode> ChildNode(TNode parentNode, List<TNode> sourceNodeList)
        {
            foreach(TNode n in sourceNodeList)
            {
                if (n.PID.Equals(parentNode.ID) == true)
                {
                    yield return n;
                }
            }
        }
        /// <summary>
        /// 迭代指定节点下的叶子
        /// </summary>
        /// <param name="parentNode">指定节点</param>
        /// <param name="sourceLeafList">数据源</param>
        /// <returns>迭代返回值</returns>
        private IEnumerable<TLeaf> ChildLeaf(TNode parentNode, List<TLeaf> sourceLeafList)
        {
            foreach (TLeaf l in sourceLeafList)
            {
                if (l.PID.Equals(parentNode.ID) == true)
                {
                    yield return l;
                }
            }
        }
        /// <summary>
        /// 将指定叶子或者节点加入到树结构中
        /// </summary>
        /// <param name="parent">指定节点</param>
        /// <param name="sourceNode">节点源</param>
        /// <param name="sourceLeaf">叶子源</param>
        private void BindNodeLeaf(TNode parent, List<TNode> sourceNode, List<TLeaf> sourceLeaf)
        {
            //取得Node子项队列
            foreach (TNode node in ChildNode(parent, sourceNode))
            {
                if (BuildFilter.FilterNode(node) == false)
                {
                    continue;
                }
                node.Deep = parent.Deep + 1;
                node.NodeType = NodeType.Node;
                node.Parent = parent;
                parent.Add(node);
                //sourceNode.Remove(node);
                //递归
                BindNodeLeaf(node, sourceNode, sourceLeaf);
            }
            //取得Leaf子项队列
            foreach (TLeaf leaf in ChildLeaf(parent, sourceLeaf))
            {
                if (BuildFilter.FilterLeaf(leaf) == false)
                {
                    continue;
                }
                leaf.Deep = parent.Deep + 1;
                leaf.NodeType = NodeType.Leaf;
                leaf.Parent = parent;
                parent.Add(leaf);
                //sourceLeaf.Remove(leaf);
            }
        }


        #region --查找某个节点--
        /// <summary>
        /// 查找相关节点信息，可能是节点，也可能是叶子
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public BaseLeaf<TKey> FindNode(Func<BaseLeaf<TKey>, bool> predicate)
        {
            return FindChildrenNode(TreeRoot, predicate);
        }

        /// <summary>
        /// 递归的查找子节点。此方法深度优先
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected BaseLeaf<TKey> FindChildrenNode(BaseNode<TKey> parent, Func<BaseLeaf<TKey>, bool> predicate)
        {
            // 传入节点检查（主要处理根节点）
            if (predicate(parent))
            {
                return parent;
            }
            foreach (var leaf in parent.ChildrenNodes.Foreach())
            {
                // 如果有符合的子节点返回
                if (predicate(leaf))
                {
                    return leaf;
                }
                // 没有则去下层检查
                return FindChildrenNode(leaf as BaseNode<TKey>, predicate);

            }
            return null;
        }
        #endregion

        #region --获取给定节点的所有父级节点--
        private void FindParents(List<BaseNode<TKey>> result, BaseNode<TKey> parent, BaseNode<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> func = null)
        {
            
        }
        #endregion
    }
}
