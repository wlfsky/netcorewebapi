using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Core.Common
{
    public class DepatmentTree : BaseTree<int, Depatment, Employee>
    {
        public DepatmentTree()
        {
            this.PreString = (i, s) =>
            {
                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < i; x++)
                {
                    sb.Append(s);
                }
                return sb.ToString();
            };
            this.SourceNodeList = new List<Depatment>();
            this.TreeRoot = new Depatment() { ID = 1, PID = 0, Name = "1x" };
            this.SourceNodeList.Add(this.TreeRoot);
            this.SourceNodeList.Add(new Depatment() { ID = 2, PID = 1, Name = "2x" });
            this.SourceNodeList.Add(new Depatment() { ID = 3, PID = 1, Name = "3x" });
            this.SourceNodeList.Add(new Depatment() { ID = 4, PID = 1, Name = "4x" });
            this.SourceNodeList.Add(new Depatment() { ID = 5, PID = 2, Name = "5x" });
            this.SourceNodeList.Add(new Depatment() { ID = 6, PID = 5, Name = "6x" });
            this.SourceNodeList.Add(new Depatment() { ID = 7, PID = 5, Name = "7x" });
            this.SourceNodeList.Add(new Depatment() { ID = 8, PID = 3, Name = "8x" });
            this.SourceNodeList.Add(new Depatment() { ID = 9, PID = 3, Name = "9x" });

            this.SourceLeafList = new List<Employee>();
            this.SourceLeafList.Add(new Employee() { ID = 11, PID = 0, Name = "1e" });
            this.SourceLeafList.Add(new Employee() { ID = 12, PID = 1, Name = "2e" });
            this.SourceLeafList.Add(new Employee() { ID = 13, PID = 1, Name = "3e" });
            this.SourceLeafList.Add(new Employee() { ID = 14, PID = 1, Name = "4e" });
            this.SourceLeafList.Add(new Employee() { ID = 15, PID = 2, Name = "5e" });
            this.SourceLeafList.Add(new Employee() { ID = 16, PID = 5, Name = "6e" });
            this.SourceLeafList.Add(new Employee() { ID = 17, PID = 5, Name = "7e" });
            this.SourceLeafList.Add(new Employee() { ID = 18, PID = 6, Name = "8e" });
            this.SourceLeafList.Add(new Employee() { ID = 19, PID = 6, Name = "9e" });

            this.Build();

        }
    }
}
