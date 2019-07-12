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

    }
}
