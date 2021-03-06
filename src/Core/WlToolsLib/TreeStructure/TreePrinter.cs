﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 树打印器。用json时，可以不要这个
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    public class TreePrinter<TKey, TNode, TLeaf> : ITreePrinter<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>
    {
        /// <summary>
        /// 叶子列表源
        /// </summary>
        public List<TLeaf> SourceLeafList { get; set; }
        /// <summary>
        /// 节点列表源
        /// </summary>
        public List<TNode> SourceNodeList { get; set; }
        /// <summary>
        /// 树结构根节点
        /// </summary>
        public TNode TreeRoot { get; set; }
        /// <summary>
        /// 缩进前置字符串
        /// </summary>
        public Func<int, string, string> PreString { get; set; }
        /// <summary>
        /// 输出字符串
        /// </summary>
        private StringBuilder outPutStr = new StringBuilder();
        /// <summary>
        /// 显示过滤器
        /// </summary>
        public IShowFilter<TKey, TNode, TLeaf> ShowFilter { get; set; }


        public TreePrinter()
        {
            if (ShowFilter == null)
            {
                ShowFilter = new DefaultShowFilter<TKey, TNode, TLeaf>();
            }
        }

        /// <summary>
        /// 打印树
        /// </summary>
        /// <returns>返回字符结果</returns>
        public string Print()
        {
            PrintNode(TreeRoot, 0);
            return outPutStr.ToString();
        }
        /// <summary>
        /// 迭代指定父节点下的子节点
        /// </summary>
        /// <param name="parentNode">指定的父节点</param>
        /// <returns>迭代的结果</returns>
        private IEnumerable<BaseNode<TKey>> ChildNode(TNode parentNode)
        {
            if (parentNode != null && parentNode.ChildrenNotNull())
            {
                foreach (var n in parentNode.ChildrenNodes)
                {
                    if (n is TNode)
                    {
                        yield return n as BaseNode<TKey>;
                    }
                }
            }
        }
        /// <summary>
        /// 迭代指定父节点下的叶子
        /// </summary>
        /// <param name="parentNode">指定的父节点</param>
        /// <returns>迭代的结果</returns>
        private IEnumerable<BaseLeaf<TKey>> ChildLeaf(TNode parentNode)
        {
            if (parentNode != null && parentNode.ChildrenNotNull())
            {
                foreach (var l in parentNode.ChildrenNodes)
                {
                    if (l is TLeaf)
                    {
                        yield return l as BaseLeaf<TKey>;
                    }
                }
            }
        }
        /// <summary>
        /// 打印节点
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="deep">深度值</param>
        private void PrintNode(TNode parent, int deep)
        {
            outPutStr.Append(PreString(deep, "-") + parent.Display());
            foreach (TNode node in ChildNode(parent))
            {
                if (ShowFilter.FilterNode(node) == false)
                {
                    continue;
                }
                //outPutStr.Append(DeepString(deep, "-") + n.Display());
                PrintNode(node, deep + 1);
            }
            foreach (TLeaf leaf in ChildLeaf(parent))
            {
                if (ShowFilter.FilterLeaf(leaf) == false)
                {
                    continue;
                }
                outPutStr.Append(PreString(deep + 1, "-") + leaf.Display());
            }
        }
    }
}
