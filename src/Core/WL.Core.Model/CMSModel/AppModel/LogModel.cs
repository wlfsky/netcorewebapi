using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Model.CMSModel.AppModel
{
    public class LogModel
    {
        public string Id { get; set; }
        public string Operator { get; set; }
        public string Remark { get; set; }
        public DateTime OperationTime { get; set; }
    }
}
