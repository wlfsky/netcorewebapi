using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WlToolsLib.TreeStructure;

namespace WL.Core.Common
{
    public class Employee : BaseLeaf<int>
    {
        public Employee()
        {

        }

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
            return $"Leaf:{this.ID}-{this.PID}-{this.Name}";
        }
    }
}
