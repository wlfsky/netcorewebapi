using System;
using System.Collections.Generic;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 基类叶子
    /// </summary>
    public interface BaseLeaf<TKey>
    {
        /// <summary>
        /// 父级id
        /// </summary>
        TKey PID { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        TKey ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 深度级别
        /// </summary>
        int Deep { get; set; }
        /// <summary>
        /// 类型，节点/叶子
        /// </summary>
        NodeType NodeType { get; set; }
        /// <summary>
        /// 根节点，肯定是节点类型
        /// </summary>
        BaseNode<TKey> Root { get; set; }
        /// <summary>
        /// 父级，肯定是节点类型
        /// </summary>
        BaseNode<TKey> Parent { get; set; }
        /// <summary>
        /// 标识符路径，由/和id构成形式为“/id/id/id”的唯一路径字符
        /// </summary>
        string IDPath { get; set; }
        /// <summary>
        /// 别名，唯一别名，id变化可能变化但是别名不会变化
        /// </summary>
        string Alias { get; set; }
        /// <summary>
        /// 别名路径
        /// </summary>
        string AliasPath { get; set; }
    }
}
