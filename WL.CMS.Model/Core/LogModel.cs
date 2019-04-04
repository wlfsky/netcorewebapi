using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    public class LogModel
    {
        public string Id { get; set; }
        public string Operator { get; set; }
        public string Remark { get; set; }
        public DateTime OperationTime { get; set; }
    }
}
