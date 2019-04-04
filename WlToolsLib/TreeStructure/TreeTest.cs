
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.TreeStructure
{
    /// <summary>
    /// 请用单元测试测试
    /// </summary>
    public class TreeTest
    {
        // 使用方法和测试移动到 Business 项目中 在 Department更好中
        public string test()
        {
            // 构成节点树 的 列表形态
            List<N> NL = new List<N>();
            N root = new N { ID = "1", PID = "0", Name = "root" };
            N r1 = new N { ID = "2", PID = "1", Name = "r1-N" };
            N r2 = new N { ID = "3", PID = "1", Name = "r2-N" };
            N r11 = new N { ID = "4", PID = "2", Name = "r11-N" };
            N r12 = new N { ID = "5", PID = "2", Name = "r12-N" };
            N r21 = new N { ID = "6", PID = "3", Name = "r21-N" };
            NL.Add(root);
            NL.Add(r1);
            NL.Add(r2);
            NL.Add(r11);
            NL.Add(r12);
            NL.Add(r21);

            // 构建叶子数据，列表形态
            List<L> LL = new List<L>();
            L rl = new L { ID = "7", PID = "1", Name = "RL-L" };
            L rl1 = new L { ID = "8", PID = "2", Name = "R1L-L" };
            L rl11 = new L { ID = "9", PID = "4", Name = "R11L1-L" };
            L rl12 = new L { ID = "10", PID = "4", Name = "R11L2-L" };
            L rl21 = new L { ID = "11", PID = "6", Name = "R21L1-L" };
            LL.Add(rl);
            LL.Add(rl1);
            LL.Add(rl11);
            LL.Add(rl12);
            LL.Add(rl21);

            //LL.Clear();

            // 用列表 且指定 根节点，创建 树构成器
            TreeBuilder<string, L, N> b = new TreeBuilder<string, L, N> { SourceLeafList = LL, SourceNodeList = NL, TreeRoot = root };
            // 指定构成过滤器
            b.BuildFilter = new testbFilter();
            // 构成
            b.Build();
            // 打印器，
            TreePrinter<string, L, N> p = new TreePrinter<string, L, N> { TreeRoot = root, PreString = DeepString };
            // 返回打印
            return p.Print();
        }

        /// <summary>
        /// 深度文字，缩进深度 字符串 构成
        /// </summary>
        /// <param name="deep"></param>
        /// <param name="deepChar"></param>
        /// <returns></returns>
        private static string DeepString(int deep, string deepChar)
        {
            StringBuilder temp = new StringBuilder();
            if (deep > 0)
            {
                for (int i = 0; i < deep; i++)
                {
                    temp.Append(deepChar);
                }
            }
            return temp.ToString();
        }

        /// <summary>
        /// 构建过滤器
        /// </summary>
        public class testbFilter : IBuildFilter<string, L, N>
        {
            /// <summary>
            /// 全构建
            /// </summary>
            /// <param name="leaf"></param>
            /// <returns></returns>
            public bool FilterLeaf(L leaf)
            {
                //if (leaf.ID == "10") return false;
                //else return true;
                return true;
            }
            /// <summary>
            /// 全构建
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public bool FilterNode(N node)
            {
                //if (node.ID == "6") return false;
                //else return true;
                return true;
            }
        }

        /// <summary>
        /// 构建过滤器
        /// </summary>
        public class testpFilter : IBuildFilter<string, L, N>
        {
            /// <summary>
            /// 叶子过滤器，id==10不构建
            /// </summary>
            /// <param name="leaf"></param>
            /// <returns></returns>
            public bool FilterLeaf(L leaf)
            {
                if (leaf.ID == "10") return false;
                else return true;
            }

            /// <summary>
            /// 节点过滤器，节点id==6不构建
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public bool FilterNode(N node)
            {
                if (node.ID == "6") return false;
                else return true;
            }
        }

        /// <summary>
        /// 继承节点，实例化泛型节点, 这里泛型是指定 id 的类型
        /// </summary>
        public class N : BaseNode<string>
        {
            //
        }

        /// <summary>
        /// 继承叶子，实例化泛型叶子，泛型指定id类型
        /// </summary>
        public class L : BaseLeaf<string>
        {
            //
        }
    }
}
