using System;
using System.Collections.Generic;
using System.Text;

namespace WL.CMS.Model
{
    /// <summary>
    /// 消息
    /// </summary>
    public class MsgModel : BaseModel
    {
        /// <summary>
        /// 消息id
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MsgText { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 消息是否阅读
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        /// 阅读时间
        /// </summary>
        public bool ReadTime { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime OverTime { get; set; }
    }
}
