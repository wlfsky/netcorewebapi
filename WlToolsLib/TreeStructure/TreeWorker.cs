using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WlToolsLib.Extension;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 完整树工作器，组合了组装和打印
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TNode"></typeparam>
    /// <typeparam name="TLeaf"></typeparam>
    public class TreeWorker<TKey, TNode, TLeaf> : ITreeWorker<TKey, TNode, TLeaf>
        where TNode : BaseNode<TKey>
        where TLeaf : BaseLeaf<TKey>

    {
        /// <summary>
        /// 节点源列表
        /// </summary>
        public List<TNode> SourceNodeList { get; set; }
        /// <summary>
        /// 叶子源列表
        /// </summary>
        public List<TLeaf> SourceLeafList { get; set; }
        
        /// <summary>
        /// 树根
        /// </summary>
        public TNode TreeRoot { get; set; }
        /// <summary>
        /// 深度字符委托
        /// </summary>
        public Func<int, string, string> PreString { get; set; }
        /// <summary>
        /// 输出字符串
        /// </summary>
        private StringBuilder outPutStr = new StringBuilder();
        /// <summary>
        /// 建造过滤器
        /// </summary>
        protected IBuildFilter<TKey, TNode, TLeaf> buildFilter { get; set; }
        /// <summary>
        /// 显示过滤器
        /// </summary>
        protected IShowFilter<TKey, TNode, TLeaf> showFilter { get; set; }
        //

        public TreeWorker()
        {
            if (buildFilter == null)
            {
                buildFilter = new DefaultBuildFilter<TKey, TNode, TLeaf>();
            }
            if (showFilter == null)
            {
                showFilter = new DefaultShowFilter<TKey, TNode, TLeaf>();
            }
        }

        public TreeWorker(TNode root, List<TNode> nodeList, List<TLeaf> leafList = null) : this()
        {
            this.TreeRoot = root;
            this.SourceNodeList = nodeList;
            this.SourceLeafList = leafList;
        }

        public static TreeWorker<TKey, TNode, TLeaf> CreateBuilder(BaseNode<TKey> root, List<TNode> nodeList, List<TLeaf> leafList = null)
        {
            TreeWorker<TKey, TNode, TLeaf> worker = new TreeWorker<TKey, TNode, TLeaf>(root as TNode, nodeList, leafList);
            worker.Build();
            return worker;
        }

        #region -- 构建 --
        /// <summary>
        /// 构建树结构
        /// </summary>
        public void Build()
        {
            this.BindNodeLeaf(TreeRoot, SourceNodeList, SourceLeafList);
        }
        /// <summary>
        /// 枚举指定节点的子节点
        /// </summary>
        /// <param name="parentNode">指定父节点</param>
        /// <param name="sourceNodeList">节点源</param>
        /// <returns>枚举返回值</returns>
        private IEnumerable<TNode> ChildNode(TNode parentNode, List<TNode> sourceNodeList)
        {
            foreach (TNode n in sourceNodeList)
            {
                if (n.PID.Equals(parentNode.ID) == true)
                {
                    yield return n;
                }
            }
        }
        /// <summary>
        /// 枚举指定父节点下的叶子
        /// </summary>
        /// <param name="parentNode">指定父节点</param>
        /// <param name="sourceLeafList">叶子源</param>
        /// <returns>枚举返回值</returns>
        private IEnumerable<TLeaf> ChildLeaf(TNode parentNode, List<TLeaf> sourceLeafList)
        {
            foreach (TLeaf l in sourceLeafList)
            {
                if (l.PID.Equals(parentNode.ID))
                {
                    yield return l;
                }
            }
        }

        private void BindNodeLeaf(TNode parent, List<TNode> sourceNode, List<TLeaf> sourceLeaf)
        {
            //取得Node子项队列
            foreach (TNode node in ChildNode(parent, sourceNode))
            {
                if (buildFilter.FilterNode(node) == false)
                {
                    continue;
                }
                node.Deep = parent.Deep + 1;
                node.Parent = parent;
                parent.Add(node);
                //sourceNode.Remove(node);
                BindNodeLeaf(node, sourceNode, sourceLeaf);//递归
            }
            //取得Leaf子项队列
            foreach (TLeaf leaf in ChildLeaf(parent, sourceLeaf))
            {
                if (buildFilter.FilterLeaf(leaf) == false)
                {
                    continue;
                }
                leaf.Deep = parent.Deep + 1;
                leaf.Parent = parent;
                parent.Add(leaf);
                //sourceLeaf.Remove(leaf);
            }
        }
        #endregion

        #region -- 打印 --
        /// <summary>
        /// 打印树结构
        /// </summary>
        /// <returns>树结构字符串</returns>
        public string Print()
        {
            PrintNode(TreeRoot, 0);
            return outPutStr.ToString();
        }
        /// <summary>
        /// 枚举指定父节点下的子节点
        /// </summary>
        /// <param name="parentNode">指定的父节点</param>
        /// <returns>枚举返回值</returns>
        private IEnumerable<TNode> ChildNode(TNode parentNode)
        {
            foreach (var n in parentNode.ChildrenNodes)
            {
                if (n is TNode)
                {
                    yield return n as TNode;
                }
            }
        }
        /// <summary>
        /// 枚举指定父节点下的叶子
        /// </summary>
        /// <param name="parentNode">指定的父节点</param>
        /// <returns>枚举返回值</returns>
        private IEnumerable<TLeaf> ChildLeaf(TNode parentNode)
        {
            foreach (var l in parentNode.ChildrenNodes)
            {
                if (l is TLeaf)
                {
                    yield return l as TLeaf;
                }
            }
        }
        /// <summary>
        /// 打印当前父亲节点
        /// </summary>
        /// <param name="parent">当前父亲节点</param>
        /// <param name="deep">深度值</param>
        private void PrintNode(TNode parent, int deep)
        {
            outPutStr.Append(PreString(deep, "-") + parent.Display());
            foreach (TNode node in ChildNode(parent))
            {
                if (showFilter.FilterNode(node) == false)
                {
                    continue;
                }
                //outPutStr.Append(DeepString(deep, "-") + n.Display());
                PrintNode(node, deep + 1);
            }
            foreach (TLeaf leaf in ChildLeaf(parent))
            {
                if (showFilter.FilterLeaf(leaf) == false)
                {
                    continue;
                }
                outPutStr.Append(PreString(deep + 1, "-") + leaf.Display());
            }
        }
        #endregion

        #region -- 任意节点父级队列（不包含兄弟和父级兄弟） --
        /// <summary>
        /// 获取所有的父级队列
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public List<TNode> Fathers(TLeaf child)
        {
            List<TNode> fatherList = new List<TNode>();
            GetFather(child, fatherList);
            return fatherList;
        }
        /// <summary>
        /// 数据源队列中寻找父节点
        /// </summary>
        /// <param name="childNode"></param>
        /// <returns></returns>
        private TNode Father(BaseLeaf<TKey> childNode)
        {
            TNode temp = null;
            foreach (TNode n in SourceNodeList)
            {
                if (n.ID.Equals(childNode.PID))
                {
                    temp = n;
                    break;
                }
                else
                {
                    temp = null;
                }
            }
            return temp;
        }
        /// <summary>
        /// 获取父级 递归
        /// </summary>
        /// <param name="child"></param>
        /// <param name="fathers"></param>
        private void GetFather(BaseLeaf<TKey> child, List<TNode> fathers)
        {
            TNode temp = Father(child);
            if (temp != null)
            {
                fathers.Add(temp);
                GetFather(temp as BaseLeaf<TKey>, fathers);
            }
        }
        #endregion

        #region -- 任意节点子集队列（） --
        /// <summary>
        /// 从列表结构中获取（并非从构建好的树形结构中获取）
        /// </summary>
        /// <param name="father"></param>
        /// <returns></returns>
        public TNode ChildrenTreeNode(TNode father)
        {
            return FindNode(father, TreeRoot);
        }

        /// <summary>
        /// 以给定的父节点为准寻找以下给定的子节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="fatherNode"></param>
        /// <returns></returns>
        private TNode FindNode(TNode node, TNode fatherNode)
        {
            TNode temp = null;
            foreach (TLeaf l in fatherNode.ChildrenNodes)
            {
                if (l is TNode)
                {
                    if (l.Equals(node))
                    {
                        temp = l as TNode;
                    }
                    else
                    {
                        temp = FindNode(node, l as TNode);
                    }
                }
            }
            return temp;
        }

        /// <summary>
        /// 以给定节点返回其所有子节点列表
        /// </summary>
        /// <param name="father">给定的父节点</param>
        /// <returns>返回的所有子节点列表</returns>
        public List<TNode> ChildrenNode(TNode father)
        {
            TNode temp = FindNode(father, TreeRoot);
            List<TNode> allNode = new List<TNode>();
            GetChildrenNode(temp, allNode);
            return allNode;
        }

        /// <summary>
        /// 将给定的父节点的子节点写入给定的列表
        /// </summary>
        /// <param name="father"></param>
        /// <param name="nodeList"></param>
        private void GetChildrenNode(TNode father, List<TNode> nodeList)
        {
            foreach (TLeaf l in father.ChildrenNodes)
            {
                if (l is TNode)
                {
                    nodeList.Add(l as TNode);
                    GetChildrenNode(l as TNode, nodeList);
                }
            }
        }

        /// <summary>
        /// 获取给定父节点的所有子叶子
        /// </summary>
        /// <param name="father">给定的父节点</param>
        /// <returns>返回叶子列表</returns>
        public List<TLeaf> ChildrenLeaf(TNode father)
        {
            TNode temp = FindNode(father, TreeRoot);
            List<TLeaf> allLeaf = new List<TLeaf>();
            GetChildrenLeaf(temp, allLeaf);
            return allLeaf;
        }

        /// <summary>
        /// 将给定父节点的叶子保存进给定的列表
        /// </summary>
        /// <param name="father">给定父节点</param>
        /// <param name="leafList">保存叶子的列表</param>
        private void GetChildrenLeaf(TNode father, List<TLeaf> leafList)
        {
            foreach(TLeaf l in father.ChildrenNodes)
            {
                if (l is TNode)
                {

                    GetChildrenLeaf(l as TNode, leafList);
                }
                else
                {
                    leafList.Add(l);
                }
            }
        }

        #endregion
        
        #region --通过路径字符串查找--
        /// <summary>
        /// 定位路径字符串，/name/name 这种风格
        /// 算法还需优化
        /// </summary>
        /// <param name="pathStr"></param>
        /// <returns></returns>
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
        #endregion

        #region --指定查找谓词查找节点--
        /// <summary>
        /// 查找指定层级（此层级必须在Tree结构中）的子级，递归层级
        /// </summary>
        /// <param name="currNode">要查找的节点入口，需在树结构中</param>
        /// <param name="predicate">查找对比规则断言</param>
        /// <returns></returns>
        public BaseLeaf<TKey> FindNode(BaseNode<TKey> currNode, Func<BaseLeaf<TKey>, bool> predicate)
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

        #region --只查找叶子--
        /// <summary>
        /// 查找符合要求的叶子
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public BaseLeaf<TKey> FindLeaf(BaseLeaf<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> predicate)
        {
            Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> newPredicate = (pt, tg) => pt is BaseLeaf<TKey> && predicate(pt, tg);
            return FindChildren(TreeRoot, target, newPredicate);
        }

        /// <summary>
        /// 递归的查找子节点。此方法深度优先
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected BaseLeaf<TKey> FindChildren(BaseNode<TKey> parent, BaseLeaf<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> predicate)
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
                    var find_res = FindChildren(leaf as BaseNode<TKey>, target, predicate);
                    if (find_res.NotNull())
                    {
                        return find_res;
                    }
                }
            }
            return null;
        }
        #endregion

        #region --查找某个节点--



        /// <summary>
        /// 查找相关节点信息，可能是节点，也可能是叶子
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public BaseLeaf<TKey> FindNode(BaseLeaf<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> predicate)
        {
            return FindChildren(TreeRoot, target, predicate);
        }
        #endregion

        #region --获取给定节点的所有父级节点--
        /// <summary>
        /// 查找目标节点，
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<BaseNode<TKey>> FindParents(BaseLeaf<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> predicate)
        {
            var node = FindNode(target, predicate);
            List<BaseNode<TKey>> parents = new List<BaseNode<TKey>>();
            ParentNode(node, parents);
            return parents;
        }

        private void ParentNode(BaseLeaf<TKey> currNode, List<BaseNode<TKey>> parents)
        {
            if (currNode.NotNull())
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

        #region --查找全部符合谓词要求的节点和叶子--

        /// <summary>
        /// 查找全部符合谓词条件的 单元（含节点和叶子）
        /// 提供对比目标，和带目标的对比谓词
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<BaseLeaf<TKey>> FindAll(BaseLeaf<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> predicate)
        {
            List<BaseLeaf<TKey>> findList = new List<BaseLeaf<TKey>>();
            Func<BaseLeaf<TKey>, bool> p = (n) => predicate(n, target);
            Action<BaseLeaf<TKey>> findAfter = (n) => findList.Add(n);
            traversingTree(TreeRoot, p, findAfter);
            return findList;
        }

        /// <summary>
        /// 查找符合谓词条件的 单元（含节点和叶子）
        /// 直接由谓词决定
        /// </summary>
        /// <param name="predicate">谓词条件</param>
        /// <returns></returns>
        public List<BaseLeaf<TKey>> FindAll(Func<BaseLeaf<TKey>, bool> predicate)
        {
            List<BaseLeaf<TKey>> findList = new List<BaseLeaf<TKey>>();
            Func<BaseLeaf<TKey>, bool> p = (n) => predicate(n);
            Action<BaseLeaf<TKey>> findAfter = (n) => findList.Add(n);
            traversingTree(TreeRoot, p, findAfter);
            return findList;
        }


        /// <summary>
        /// 查找全部符合谓词条件的 节点
        /// 需要对比对象，和对比谓词
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<BaseNode<TKey>> FindAllNode(BaseNode<TKey> target, Func<BaseNode<TKey>, BaseLeaf<TKey>, bool> predicate)
        {
            List<BaseNode<TKey>> findNodeList = new List<BaseNode<TKey>>();
            Func<BaseLeaf<TKey>, bool> p = (n) => n is BaseNode<TKey> && predicate(n as BaseNode<TKey>, target);
            Action<BaseLeaf<TKey>> findAfter = (n) => findNodeList.Add(n as BaseNode<TKey>);
            traversingTree(TreeRoot, p, findAfter);
            return findNodeList;
        }

        /// <summary>
        /// 查找全部符合谓词条件的 节点
        /// 只要提供对比谓词
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<BaseNode<TKey>> FindAllNode(Func<BaseLeaf<TKey>, bool> predicate)
        {
            List<BaseNode<TKey>> findNodeList = new List<BaseNode<TKey>>();
            Func<BaseLeaf<TKey>, bool> p = (n) => n is BaseNode<TKey> && predicate(n as BaseNode<TKey>);
            Action<BaseLeaf<TKey>> findAfter = (n) => findNodeList.Add(n as BaseNode<TKey>);
            traversingTree(TreeRoot, p, findAfter);
            return findNodeList;
        }

        /// <summary>
        /// 查找全部符合谓词条件的 叶子
        /// 需提供对比对象 和 对比谓词
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<BaseLeaf<TKey>> FindAllLeaf(BaseLeaf<TKey> target, Func<BaseLeaf<TKey>, BaseLeaf<TKey>, bool> predicate)
        {
            List<BaseLeaf<TKey>> findLeafList = new List<BaseLeaf<TKey>>();
            Func<BaseLeaf<TKey>, bool> p = (n) => n is BaseLeaf<TKey> && predicate(n, target);
            Action<BaseLeaf<TKey>> findAfter = (n) => findLeafList.Add(n);
            traversingTree(TreeRoot, p, findAfter);
            return findLeafList;
        }

        /// <summary>
        /// 查找全部符合谓词条件的 叶子
        /// 只需对比谓词
        /// </summary>
        /// <param name="target"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<BaseLeaf<TKey>> FindAllLeaf(Func<BaseLeaf<TKey>, bool> predicate)
        {
            List<BaseLeaf<TKey>> findLeafList = new List<BaseLeaf<TKey>>();
            Func<BaseLeaf<TKey>, bool> p = (n) => n is BaseLeaf<TKey> && predicate(n);
            Action<BaseLeaf<TKey>> findAfter = (n) => findLeafList.Add(n);
            traversingTree(TreeRoot, p, findAfter);
            return findLeafList;
        }


        #endregion

        #region --基础算法，遍历--
        /// <summary>
        /// 深度优先遍历
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="predicate"></param>
        /// <param name="findAfter"></param>
        private void traversingTree(BaseNode<TKey> parent, Func<BaseLeaf<TKey>, bool> predicate, Action<BaseLeaf<TKey>> findAfter)
        {
            if (parent is BaseNode<TKey> && parent.ChildrenNodes.HasItem())
            {
                foreach (var node in parent.ChildrenNodes)
                {
                    if (node is BaseNode<TKey>)
                    {
                        if (predicate(node))
                        {
                            findAfter(node);
                        }
                        traversingTree(node as BaseNode<TKey>, predicate, findAfter);
                    }
                    else // 如果是叶子
                    {
                        if (predicate(node))
                        {
                            findAfter(node);
                        }
                    }
                }

            }
        }
        #endregion
    }
}
