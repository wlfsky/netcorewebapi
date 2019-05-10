using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 基础节点扩展方法
    /// </summary>
    public static class BaseLeafExtension
    {
        /// <summary>
        /// 节点是否空节点，空返回true
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NullNode<TKey>(this BaseNode<TKey> self)
        {
            if(self == null)
            { return true; }
            else { return false; }
        }

        /// <summary>
        /// 节点是否空节点，非空返回true
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool NotNullNode<TKey>(this BaseNode<TKey> self)
        {
            return !self.NullNode();
        }

        /// <summary>
        /// 给定节点的子级是否空，非空返回true
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChildrenNotNull<TKey>(this BaseNode<TKey> self)
        {
            if (self.NotNullNode() && self.ChildrenNodes!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 给定节点的子级是否空，空返回true
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChildrenNull<TKey>(this BaseNode<TKey> self)
        {
            if (self.NullNode())
            {
                return true;
            }
            else
            {
                if (self.ChildrenNodes == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 检查给定节点是否有子节点
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool HaveChildrenNode<TKey>(this BaseNode<TKey> self)
        {
            if (self != null && self.ChildrenNodes != null && self.ChildrenNodes.Any())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 给一个节点加入一个子集，不确定是节点还是叶子
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="node"></param>
        /// <param name="newNode"></param>
        public static void Add<TKey>(this BaseNode<TKey> node, BaseLeaf<TKey> newNode)
        {
            if (node.ChildrenNull())
            {
                node.ChildrenNodes = new List<BaseLeaf<TKey>>();
            }
            node.ChildrenNodes.Add(newNode);
        }

        /// <summary>
        /// 给一个node加入一个字节点node
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="node"></param>
        /// <param name="newNode"></param>
        public static void AddNode<TKey>(this BaseNode<TKey> node, BaseNode<TKey> newNode)
        {
            if (node.ChildrenNull())
            {
                node.ChildrenNodes = new List<BaseLeaf<TKey>>();
            }
            node.ChildrenNodes.Add(newNode);
        }

        /// <summary>
        /// 给一个node加入一个叶子
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="node"></param>
        /// <param name="newLeaf"></param>
        public static void AddLeaf<TKey>(this BaseNode<TKey> node, BaseLeaf<TKey> newLeaf)
        {
            if (node.ChildrenNull())
            {
                node.ChildrenNodes = new List<BaseLeaf<TKey>>();
            }
            node.ChildrenNodes.Add(newLeaf);
        }

        /// <summary>
        /// 返回叶子节点的字符形式
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string Display<TKey>(this BaseLeaf<TKey> self)
        {
            if (self != null)
            {
                return $"{self.ID} - {self.PID} - {self.Name} - {self.NodeType}";
            }
            else
            {
                return $"Object is null";
            }
        }
    }
}
