using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.TreeStructure;
using WlToolsLib.Extension;

namespace WL.Core.Common
{
    /// <summary>
    /// 基础树
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    public class BaseTree<TKey, TNode, TLeaf> : TreeWorker<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey> 
    {
        public BaseTree()
        {

        }

        public BaseLeaf<TKey> LocationPath(string pathStr)
        {
            var pathArray = pathStr.Split('/');
            BaseLeaf<TKey> thisNode = this.TreeRoot;
            if (pathArray.Any())
            {
                for (var i = 0; i < pathArray.Length; i++)
                {
                    if (i == 0 && pathArray[i].NullEmpty())
                    {
                        continue;
                    }

                    if (pathArray[i].NotNullEmpty() && thisNode.NotNull())
                    {
                        var nname = pathArray[i];
                        thisNode = FindNodeByName(nname, thisNode);
                        if (thisNode.IsNull())
                        {
                            break;
                        }
                    }
                }
            }
            return thisNode;
        }

        protected BaseLeaf<TKey> FindNodeByName(string name, BaseLeaf<TKey> currNode)
        {
            if (currNode.IsNull()) { return default(TNode); }
            if (currNode.Name.Equals(name)) { return currNode; }
            if (currNode is TNode)
            {
                var n = currNode as TNode;
                if (n.ChildrenNodes.HasItem())
                {
                    foreach (var item in n.ChildrenNodes)
                    {
                        if (item.Name.Equals(name))
                        {
                            return item as BaseLeaf<TKey>;
                        }
                    }
                }
            }
            else
            {
                if (currNode.Name.Equals(name))
                {
                    return currNode;
                }
            }
            return default(TNode);
        }

        protected BaseLeaf<TKey> FindNodeByName(string name, List<TNode> currNode)
        {
            if (currNode.HasItem())
            {
                foreach (var item in currNode)
                {
                    if (item.Name.Equals(name))
                    {
                        return item;
                    }
                    if (item is TNode)
                    {
                        return FindNodeByName(name, item);
                    }
                }
            }
            return default(TNode);
        }

        #region --查找节点--
        /// <summary>
        /// 寻找下一层，递归层级
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="findAction"></param>
        /// <returns></returns>
        public BaseLeaf<TKey> FindNode(TNode currNode, Func<BaseLeaf<TKey>, bool> predicate)
        {
            if (predicate.IsNull())
            {
                return default(TLeaf);
            }
            if (currNode.ChildrenNodes.HasItem())
            {
                foreach (var item in currNode.ChildrenNodes)
                {
                    if (item is TLeaf)
                    {
                        var l = item as TLeaf;
                        if (predicate(l))
                        {
                            return l;
                        }
                    }
                    if (item is TNode)
                    {
                        var n = item as TNode;
                        var l = n as BaseLeaf<TKey>;
                        if (predicate(l))
                        {
                            return l;
                        }
                        var findResult = FindNode(n, predicate);
                        if (findResult.NotNull())
                        {
                            return findResult;
                        }
                    }
                }
            }
            return default(TLeaf);
        }

        /// <summary>
        /// 寻找当前子级层级，但是不递归
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="findAction"></param>
        /// <returns></returns>
        public TLeaf FindCurrLevel(TNode currNode, Func<TLeaf, bool> predicate)
        {
            if (predicate.IsNull())
            {
                return default(TLeaf);
            }
            if (currNode.ChildrenNodes.HasItem())
            {
                foreach (var item in currNode.ChildrenNodes)
                {
                    if (item is TLeaf)
                    {
                        var l = item as TLeaf;
                        if (predicate(l))
                        {
                            return l;
                        }
                    }
                    if (item is TNode)
                    {
                        var n = item as TNode;
                        if (predicate(n as TLeaf))
                        {
                            return n as TLeaf;
                        }
                    }

                }
            }

            return default(TLeaf);
        }
        #endregion

    }
}
