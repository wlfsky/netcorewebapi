using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.TreeStructure;

namespace WL.Core.Common
{
    public class Depatment : BaseNode<int>
    {
        public Depatment()
        {

        }

        public new List<Employee> ChildrenNode { get; set; }
        public List<BaseLeaf<int>> ChildrenNodes { get; set; }
        public int PID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Deep { get; set; }
        public NodeType NodeType { get; set; }
        public BaseNode<int> Root { get; set; }
        public BaseNode<int> Parent { get; set; }
        public string IDPath { get; set; }
        public string Alias { get; set; }
        public string AliasPath { get; set; }

        public override string ToString()
        {
            return $"Node:{this.ID}-{this.PID}-{this.Name}";
        }
    }
}
