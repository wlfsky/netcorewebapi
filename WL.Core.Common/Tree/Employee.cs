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

        public override string ToString()
        {
            return $"Leaf:{this.ID}-{this.PID}-{this.Name}";
        }
    }
}
