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
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    public class TreeBuilder<TKey, TNode, TLeaf> : ITreeBuilder<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
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
        public IBuildFilter<TKey, TNode, TLeaf> BuildFilter { get; set; }


        public TreeBuilder()
        {
            if (BuildFilter == null)
            {
                BuildFilter = new DefaultBuildFilter<TKey, TNode, TLeaf>();
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
        public BaseLeaf<TKey> FindNode(BaseLeaf<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> predicate)
        {
            return FindChildrenNode(TreeRoot, target, predicate);
        }

        /// <summary>
        /// 递归的查找子节点。此方法深度优先
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected BaseLeaf<TKey> FindChildrenNode(BaseNode<TKey> parent, BaseLeaf<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> predicate)
        {
            // 传入节点检查（主要处理根节点）
            if (predicate(parent, target))
            {
                return parent;
            }
            foreach (var leaf in parent.ChildrenNodes.Foreach())
            {
                // 如果有符合的子节点返回
                if (predicate(leaf, target))
                {
                    return leaf;
                }
                // 没有则去下层检查
                // 去下层前检查一下是 node 再去
                if (leaf is BaseNode<TKey>)
                {
                    var find_res = FindChildrenNode(leaf as BaseNode<TKey>, target, predicate);
                    if (find_res.NotNull())
                    {
                        return find_res;
                    }
                }
            }
            return null;
        }
        #endregion

        #region --获取给定节点的所有父级节点--
        /// <summary>
        /// 查找目标节点，
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<BaseNode<TKey>> FindParents(BaseLeaf<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> predicate = null)
        {
            var node = FindNode(target, predicate);
            List<BaseNode<TKey>> parents = new List<BaseNode<TKey>>();
            ParentNode(node, parents);
            return parents;
        }

        private void ParentNode(BaseLeaf<TKey> currNode, List<BaseNode<TKey>> parents)
        {
            if(currNode.NotNull())
            {
                if (currNode.Parent.NotNull())
                {
                    ParentNode(currNode.Parent, parents);
                }
                // 检查一下找到的是节点还是叶子，叶子不应该被加入父级层级组内
                if (currNode is BaseNode<TKey>)
                {
                    parents.Add(currNode as BaseNode<TKey>);
                }
            }
            
        }
        #endregion
    }
}
