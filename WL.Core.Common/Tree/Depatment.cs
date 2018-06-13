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

        public override string ToString()
        {
            return $"Node:{this.ID}-{this.PID}-{this.Name}";
        }
    }
}
