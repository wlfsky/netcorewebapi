using System;
using System.Collections.Generic;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 基类叶子
    /// </summary>
    public abstract class BaseLeaf<TKey> : IDisplay, IEquatable<BaseLeaf<TKey>>
    {
        /// <summary>
        /// 父级id
        /// </summary>
        public TKey PID { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public TKey ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 深度级别
        /// </summary>
        public int Deep { get; set; }
        /// <summary>
        /// 类型，节点/叶子
        /// </summary>
        public NodeType NodeType { get; set; }
        /// <summary>
        /// 父级，肯定是节点类型
        /// </summary>
        public BaseNode<TKey> Parent { get; set; }
        /// <summary>
        /// 标识符路径，由/和id构成形式为“/id/id/id”的唯一路径字符
        /// </summary>
        public string IDPath { get; set; }
        /// <summary>
        /// 别名，唯一别名，id变化可能变化但是别名不会变化
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 别名路径
        /// </summary>
        public string AliasPath { get; set; }

        /// <summary>
        /// 空初始化
        /// </summary>
        public BaseLeaf()
        {
            this.NodeType = NodeType.Leaf;
        }
        /// <summary>
        /// 显示方法
        /// </summary>
        /// <returns></returns>
        public string Display()
        {
            return string.Format("ID:{0}  PID:{1}  Name:{2} \r\n", ID, PID, Name);
        }
        /// <summary>
        /// 是否相等，对比id
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(BaseLeaf<TKey> other)
        {
            if (other.ID.Equals(this.ID) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
