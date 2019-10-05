using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using WL.Core.DBModel;

namespace WL.Account.Model.DB
{
    [Table("WL_OpearationLog")]
    public class OpearationLog : BaseDBModel
    {
        public string LogID { get; set; }
        public string OperatorID { get; set; }
        public string Operator { get; set; }
        public DateTime OperatingTime { get; set; }
    }
}
