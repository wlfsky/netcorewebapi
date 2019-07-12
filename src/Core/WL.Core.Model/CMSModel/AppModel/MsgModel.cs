using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Core.Model.CMSModel.AppModel
{
    public class MsgModel
    {
        public string MsgId { get; set; }
        public string MsgText { get; set; }
        public DateTime MsgTime { get; set; }
        public bool IsRead { get; set; }
    }
}
